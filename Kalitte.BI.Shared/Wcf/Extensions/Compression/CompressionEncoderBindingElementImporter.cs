using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Description;
using System.Xml;

namespace Kalitte.BI.Shared.Wcf.Extensions.Compression
{
    class CompressionEncoderBindingElementImporter: IPolicyImportExtension
    {
        public CompressionEncoderBindingElementImporter()
        {

        }

        void IPolicyImportExtension.ImportPolicy(MetadataImporter importer, PolicyConversionContext context)
        {
            if (importer == null)
            {
                throw new ArgumentNullException("importer");
            }

            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            ICollection<XmlElement> assertions = context.GetBindingAssertions();
            foreach (XmlElement assertion in assertions)
            {
                if ((assertion.NamespaceURI == CompressionEncoderPolicyConstants.GZipEncodingNamespace) &&
                    (assertion.LocalName == CompressionEncoderPolicyConstants.GZipEncodingName)
                    )
                {
                    assertions.Remove(assertion);
                    context.BindingElements.Add(new CompressionEncoderBindingElement());
                    break;
                }
            }
        }
    }
}
