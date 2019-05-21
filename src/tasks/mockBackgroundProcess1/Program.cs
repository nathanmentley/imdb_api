using System;
using System.Text;

using Apache.NMS;
using Apache.NMS.Util;

namespace mockBackgroundProcess1
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri connecturi = new Uri("activemq:tcp://activemq:61616");
            
            // NOTE: ensure the nmsprovider-activemq.config file exists in the executable folder.
            IConnectionFactory factory = new NMSConnectionFactory(connecturi);

            using(IConnection connection = factory.CreateConnection())
            using(ISession session = connection.CreateSession())
            {
                IDestination destination = SessionUtil.GetDestination(session, "queue://FOO.BAR");

                // Create a consumer and producer
                using(IMessageConsumer consumer = session.CreateConsumer(destination))
                {
                    // Start the connection so that messages will be processed.
                    connection.Start();
                        
                    // Consume a message
                    while(true) {
                        ITextMessage message = consumer.Receive() as ITextMessage;
                        if(message == null)
                        {
                            Console.WriteLine("No message received!");
                        }
                        else
                        {
                            message.Acknowledge();

                            Console.WriteLine("Received message with ID:   " + message.NMSMessageId);
                            Console.WriteLine("Received message with text: " + message.Text);
                        }
                    }
                }
            }
        }
    }
}
