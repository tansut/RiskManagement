using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;
using System.Configuration;
using System.Xml;

namespace Kalitte.BI.Shared.Wcf.Extensions.Compression
{
    // This is constants for GZip message encoding policy.
    static class CompressionEncoderPolicyConstants
    {
        public const string GZipEncodingName = "GZipEncoding";
        public const string GZipEncodingNamespace = "http://schemas.microsoft.com/ws/06/2004/mspolicy/netgzip1";
        public const string GZipEncodingPrefix = "gzip";
    }

    //This is the binding element that, when plugged into a custom binding, will enable the GZip encoder
    public sealed class CompressionEncoderBindingElement
                        : MessageEncodingBindingElement //BindingElement
                        , IPolicyExportExtension
    {

        //We will use an inner binding element to store information required for the inner encoder
        MessageEncodingBindingElement innerBindingElement;

        //By default, use the default text encoder as the inner encoder
        public CompressionEncoderBindingElement()
            : this(new TextMessageEncodingBindingElement()) { }

        public CompressionEncoderBindingElement(MessageEncodingBindingElement messageEncoderBindingElement)
        {
            this.innerBindingElement = messageEncoderBindingElement;
        }

        public MessageEncodingBindingElement InnerMessageEncodingBindingElement
        {
            get { return innerBindingElement; }
            set { innerBindingElement = value; }
        }

        //Main entry point into the encoder binding element. Called by WCF to get the factory that will create the
        //message encoder
        public override MessageEncoderFactory CreateMessageEncoderFactory()
        {
            return new CompressionEncoderFactory(innerBindingElement.CreateMessageEncoderFactory());
        }

        public override MessageVersion MessageVersion
        {
            get { return innerBindingElement.MessageVersion; }
            set { innerBindingElement.MessageVersion = value; }
        }

        public override BindingElement Clone()
        {
            return new CompressionEncoderBindingElement(this.innerBindingElement);
        }

        public override T GetProperty<T>(BindingContext context)
        {
            if (typeof(T) == typeof(XmlDictionaryReaderQuotas))
            {
                return innerBindingElement.GetProperty<T>(context);
            }
            else
            {
                return base.GetProperty<T>(context);
            }
        }

        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            context.BindingParameters.Add(this);
            return context.BuildInnerChannelFactory<TChannel>();
        }

        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            context.BindingParameters.Add(this);
            return context.BuildInnerChannelListener<TChannel>();
        }

        public override bool CanBuildChannelListener<TChannel>(BindingContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            context.BindingParameters.Add(this);
            return context.CanBuildInnerChannelListener<TChannel>();
        }

        void IPolicyExportExtension.ExportPolicy(MetadataExporter exporter, PolicyConversionContext policyContext)
        {
            if (policyContext == null)
            {
                throw new ArgumentNullException("policyContext");
            }
            XmlDocument document = new XmlDocument();
            policyContext.GetBindingAssertions().Add(document.CreateElement(
                CompressionEncoderPolicyConstants.GZipEncodingPrefix,
                CompressionEncoderPolicyConstants.GZipEncodingName,
                CompressionEncoderPolicyConstants.GZipEncodingNamespace));
        }
    }

    //This class is necessary to be able to plug in the GZip encoder binding element through
    //a configuration file
    public class CompressionEncoderElement : BindingElementExtensionElement
    {
        public CompressionEncoderElement()
        {
        }

        //Called by the WCF to discover the type of binding element this config section enables
        public override Type BindingElementType
        {
            get { return typeof(CompressionEncoderBindingElement); }
        }

        //The only property we need to configure for our binding element is the type of
        //inner encoder to use. Here, we support text and binary.
        [ConfigurationProperty("innerMessageEncoding", DefaultValue = "textMessageEncoding")]
        public string InnerMessageEncoding
        {
            get { return (string)base["innerMessageEncoding"]; }
            set { base["innerMessageEncoding"] = value; }
        }

        [ConfigurationProperty("readerQuotas")]
        public XmlDictionaryReaderQuotasElement ReaderQuotas
        {
            get { return (XmlDictionaryReaderQuotasElement)base["readerQuotas"]; }
        }

        //Called by the WCF to apply the configuration settings (the property above) to the binding element
        public override void ApplyConfiguration(BindingElement bindingElement)
        {
            CompressionEncoderBindingElement binding = (CompressionEncoderBindingElement)bindingElement;
            PropertyInformationCollection propertyInfo = this.ElementInformation.Properties;
            if (propertyInfo["innerMessageEncoding"].ValueOrigin != PropertyValueOrigin.Default)
            {
                switch (this.InnerMessageEncoding)
                {
                    case "textMessageEncoding":
                        binding.InnerMessageEncodingBindingElement = new TextMessageEncodingBindingElement();
                        ApplyReaderQuotasConfiguration((binding.InnerMessageEncodingBindingElement as TextMessageEncodingBindingElement).ReaderQuotas);
                        break;
                    case "binaryMessageEncoding":
                        binding.InnerMessageEncodingBindingElement = new BinaryMessageEncodingBindingElement();
                        ApplyReaderQuotasConfiguration((binding.InnerMessageEncodingBindingElement as BinaryMessageEncodingBindingElement).ReaderQuotas);
                        break;
                }
            }
            else
                ApplyReaderQuotasConfiguration((binding.InnerMessageEncodingBindingElement as TextMessageEncodingBindingElement).ReaderQuotas);
        }

        private void ApplyReaderQuotasConfiguration(XmlDictionaryReaderQuotas readerQuotas)
        {
            XmlDictionaryReaderQuotasElement readerQuotasElement = ReaderQuotas;
            
            if (readerQuotasElement.MaxArrayLength != 0)
                readerQuotas.MaxArrayLength = readerQuotasElement.MaxArrayLength;

            if (readerQuotasElement.MaxBytesPerRead != 0)
                readerQuotas.MaxBytesPerRead = readerQuotasElement.MaxBytesPerRead;

            if (readerQuotasElement.MaxDepth != 0)
                readerQuotas.MaxDepth = readerQuotasElement.MaxDepth;

            if (readerQuotasElement.MaxNameTableCharCount != 0)
                readerQuotas.MaxNameTableCharCount = readerQuotasElement.MaxNameTableCharCount;

            if (readerQuotasElement.MaxStringContentLength != 0)
                readerQuotas.MaxStringContentLength = readerQuotasElement.MaxStringContentLength;
        }

        //Called by the WCF to create the binding element
        protected override BindingElement CreateBindingElement()
        {
            CompressionEncoderBindingElement bindingElement = new CompressionEncoderBindingElement();
            this.ApplyConfiguration(bindingElement);
            return bindingElement;
        }
    }


}
