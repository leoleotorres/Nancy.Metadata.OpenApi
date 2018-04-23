﻿using Nancy.Metadata.OpenApi.Model;
using Nancy.Metadata.OpenApi.Tests.Fakes;
using Nancy.Routing;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using Xunit;

namespace Nancy.Metadata.OpenApi.Tests.UnitTests
{
    public class ModuleTests
    {
        [Fact]
        public void Generate_docs()
        {
            //Arrange
            var module = new FakeModule(new DefaultRouteCacheProvider(() => new FakeRouteCache()));

            //Act
            var response = module.GetDocumentation();
            string body;

            using (var memoryStream = new MemoryStream())
            {
                response.Contents.Invoke(memoryStream);
                body = Encoding.ASCII.GetString(memoryStream.GetBuffer());
            }

            var spec = JsonConvert.DeserializeObject<OpenApiSpecification>(body);

            //Assert
            Assert.NotNull(spec.Api);
            Assert.NotNull(spec.Servers);
            Assert.Equal(FakeModule.Server.Description, spec.Servers[0].Description);
            Assert.Equal(FakeModule.Server.Url, spec.Servers[0].Url);
            Assert.Equal(FakeModule.Title, spec.Api.Title);
            Assert.Equal(FakeModule.ApiVersion, spec.Api.Version);
            Assert.Equal(FakeModule.TermsOfService, spec.Api.TermsOfService);
        }

        [Fact]
        public void Generate_docs_with_contact_info()
        {
            //Arrange
            var module = new FakeModule(new DefaultRouteCacheProvider(() => new FakeRouteCache()));
            var contact = new FakeContact();

            //Act
            module.FillContact(contact.Name, contact.Email, contact.Url);

            var response = module.GetDocumentation();

            string body;

            using (var memoryStream = new MemoryStream())
            {
                response.Contents.Invoke(memoryStream);
                body = Encoding.ASCII.GetString(memoryStream.GetBuffer());
            }

            var spec = JsonConvert.DeserializeObject<OpenApiSpecification>(body);

            //Assert
            Assert.NotNull(spec.Api);
            Assert.NotNull(spec.Api.Contact);
            Assert.Equal(contact.Name, spec.Api.Contact.Name);
            Assert.Equal(contact.Email, spec.Api.Contact.Email);
            Assert.Equal(contact.Url, spec.Api.Contact.Url);
        }

        [Fact]
        public void Generate_docs_with_external_doc()
        {
            //Arrange
            var module = new FakeModule(new DefaultRouteCacheProvider(() => new FakeRouteCache()));
            var doc = new FakeExternalDoc();

            //Act
            module.FillExternalDoc(doc.Description, doc.Url);

            var response = module.GetDocumentation();

            string body;

            using (var memoryStream = new MemoryStream())
            {
                response.Contents.Invoke(memoryStream);
                body = Encoding.ASCII.GetString(memoryStream.GetBuffer());
            }

            var spec = JsonConvert.DeserializeObject<OpenApiSpecification>(body);

            //Assert
            Assert.NotNull(spec.ExternalDocs);
            Assert.Equal(doc.Description, spec.ExternalDocs.Description);
            Assert.Equal(doc.Url, spec.ExternalDocs.Url);
        }

        [Fact]
        public void Generate_docs_with_License()
        {
            //Arrange
            var module = new FakeModule(new DefaultRouteCacheProvider(() => new FakeRouteCache()));
            var lic = new FakeLicense();

            //Act
            module.FillLicense(lic.Name, lic.Url);

            var response = module.GetDocumentation();

            string body;

            using (var memoryStream = new MemoryStream())
            {
                response.Contents.Invoke(memoryStream);
                body = Encoding.ASCII.GetString(memoryStream.GetBuffer());
            }

            var spec = JsonConvert.DeserializeObject<OpenApiSpecification>(body);

            //Assert
            Assert.NotNull(spec.Api);
            Assert.NotNull(spec.Api.License);
            Assert.Equal(lic.Name, spec.Api.License.Name);
            Assert.Equal(lic.Url, spec.Api.License.Url);
        }
    }
}