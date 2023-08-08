using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Channels;

namespace Kalitte.BI.Shared.Wcf.Extensions.Compression
{
    class CompressionEncoderFactory: MessageEncoderFactory
    {
        private MessageEncoder encoder;

        //The GZip encoder wraps an inner encoder
        //We require a factory to be passed in that will create this inner encoder
        public CompressionEncoderFactory(MessageEncoderFactory messageEncoderFactory)
        {
            if (messageEncoderFactory == null)
                throw new ArgumentNullException("messageEncoderFactory", "A valid message encoder factory must be passed to the GZipEncoder");
            
            encoder = new GZipMessageEncoder(messageEncoderFactory.Encoder);
        }

        //The service framework uses this property to obtain an encoder from this encoder factory
        public override MessageEncoder Encoder
        {
            get { return encoder; }
        }

        public override MessageVersion MessageVersion
        {
            get { return encoder.MessageVersion; }
        }
    }
}
