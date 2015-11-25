using Microsoft.Azure.Documents;
using Microsoft.Owin;
using Owin;
using SmartSpaceWeb.Models;

[assembly: OwinStartupAttribute(typeof(SmartSpaceWeb.Startup))]
namespace SmartSpaceWeb
{
    public partial class Startup
    {
        public async void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
//            string body = @"function validate() {
//                        var context = getContext();
//                        var request = context.getRequest();                                                             
//                        var documentToCreate = request.getBody();

//                        // validate properties
//                        if (!('timestamp' in documentToCreate)) {
//                            var ts = new Date();
//                            documentToCreate['timestamp'] = ts.getTime();
//                        }

//                        // update the document that will be created
//                        request.setBody(documentToCreate);

//======


//var context = getContext();
//        var collection = context.getCollection();
//        var response = context.getResponse();

//        // document that was created
//        var createdDocument = response.getBody();

//        // query for metadata document
//        var filterQuery = 'SELECT * FROM root r WHERE r.id = ""_metadata""';
//        var accept = collection.queryDocuments(collection.getSelfLink(), filterQuery,
//            updateMetadataCallback);
//            if (!accept) throw ""Unable to update metadata, abort"";

//            function updateMetadataCallback(err, documents, responseOptions) {
//            if (err) throw new Error(""Error"" + err.message);
//            if (documents.length != 1) throw 'Unable to find metadata document';

//            var metadataDocument = documents[0];

//            // update metadata
//            metadataDocument.createdDocuments += 1;
//            metadataDocument.createdNames += "" "" + createdDocument.id;
//            var accept = collection.replaceDocument(metadataDocument._self,
//                  metadataDocument, function(err, docReplaced) {
//                if (err) throw ""Unable to update metadata, abort"";
//            });
//            if (!accept) throw ""Unable to update metadata, abort"";
//            return;
//        }";
            
//            await DocumentDBRepository<Sensor>.CreateTriggerAsync("CreateTriggerTest", body, TriggerType.Post, TriggerOperation.Create);
        }
    }
}
