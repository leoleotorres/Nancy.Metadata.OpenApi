﻿using Nancy.Metadata.OpenApi.Fluent;
using Nancy.Metadata.OpenApi.Model;
using Nancy.Metadata.OpenApi.Tests.Fakes;
using Xunit;

namespace Nancy.Metadata.OpenApi.Tests.UnitTests
{
    public class EndpointInfoTests
    {
        [Fact]
        public void Endpoint_with_summary()
        {
            //Arrange
            var fakeEndpoint = new FakeEndpoint();

            //Act
            var endpoint = new Endpoint(fakeEndpoint.OperationName).WithSummary(fakeEndpoint.Summary);

            //Assert
            Assert.Equal(fakeEndpoint.Summary, endpoint.Summary);
        }

        [Fact]
        public void Endpoint_with_description()
        {
            //Arrange
            var fakeEndpoint = new FakeEndpoint();

            //Act
            var endpoint = new Endpoint(fakeEndpoint.OperationName).WithDescription(fakeEndpoint.Description);

            //Assert
            Assert.Equal(fakeEndpoint.Description, endpoint.Description);
        }

        [Fact]
        public void Endpoint_with_external_documentation()
        {
            //Arrange
            var fakeEndpoint = new FakeEndpoint();

            //Act
            var endpoint = new Endpoint(fakeEndpoint.OperationName)
                .WithExternalDocumentation(fakeEndpoint.ExternalDocsUrl, fakeEndpoint.ExternalDocs);

            //Assert
            Assert.Equal(fakeEndpoint.ExternalDocs, endpoint.ExternalDocs.Description);
            Assert.Equal(fakeEndpoint.ExternalDocsUrl, endpoint.ExternalDocs.Url);
        }

        [Fact]
        public void Endpoint_with_is_deprecated_flag()
        {
            //Arrange
            var fakeEndpoint = new FakeEndpoint();

            //Act
            var endpoint = new Endpoint(fakeEndpoint.OperationName).WithDeprecatedFlag();

            //Assert
            Assert.True(endpoint.IsDeprecated);
        }

        [Fact]
        public void Endpoint_with_description_and_tags()
        {
            //Arrange
            var fakeEndpoint = new FakeEndpoint();

            //Act
            var endpoint = new Endpoint(fakeEndpoint.OperationName).WithDescription(fakeEndpoint.Description, fakeEndpoint.Tags);

            //Assert
            Assert.Equal(fakeEndpoint.Description, endpoint.Description);
            Assert.Equal(fakeEndpoint.Tags, endpoint.Tags);
        }

        [Fact]
        public void Endpoint_with_request()
        {
            //Arrange
            var fakeEndpoint = new FakeEndpoint();
            var fakeRequest = new FakeRequest();

            //Act
            var endpoint = new Endpoint(fakeEndpoint.OperationName)
                .WithRequestParameter(
                fakeRequest.Name,
                fakeRequest.Type,
                fakeRequest.Format,
                fakeRequest.Required,
                fakeRequest.Description,
                fakeRequest.Loc);

            //Assert
            Assert.Equal(fakeRequest.Description, endpoint.RequestParameters[0].Description);
            Assert.Equal(fakeRequest.Format, endpoint.RequestParameters[0].Format);
            Assert.Equal(fakeRequest.Required, endpoint.RequestParameters[0].Required);
            Assert.Equal(fakeRequest.Name, endpoint.RequestParameters[0].Name);
            Assert.Equal(fakeRequest.Loc, endpoint.RequestParameters[0].In);
            Assert.Equal(fakeRequest.Type, endpoint.RequestParameters[0].Type);
        }

        [Fact]
        public void Endpoint_with_request_model()
        {
            //Arrange
            var fakeEndpoint = new FakeEndpoint();
            var fakeRequest = new FakeRequestModel();

            //Act
            var endpoint = new Endpoint(fakeEndpoint.OperationName)
                .WithRequestModel(
                    typeof(FakeRequestModel),
                    fakeRequest.Name,
                    fakeRequest.Description,
                    fakeRequest.Required,
                    fakeRequest.Loc);

            //Assert
            Assert.Equal(fakeRequest.Description, endpoint.RequestParameters[0].Description);
            Assert.Equal(fakeRequest.Required, endpoint.RequestParameters[0].Required);
            Assert.Equal(fakeRequest.Name, endpoint.RequestParameters[0].Name);
            Assert.Equal(fakeRequest.Loc, endpoint.RequestParameters[0].In);
            Assert.Contains(nameof(FakeRequestModel), endpoint.RequestParameters[0].Schema.Ref);
        }

        [Fact]
        public void Endpoint_with_response_model()
        {
            //Arrange
            var fakeEndpoint = new FakeEndpoint();
            var fakeResponseModel = new FakeResponseModel();

            //Act
            var endpoint = new Endpoint(fakeEndpoint.OperationName)
                .WithResponseModel(fakeResponseModel.StatusCode, typeof(FakeResponseModel), fakeResponseModel.Description);

            //Assert
            Assert.Equal(fakeEndpoint.OperationName, endpoint.OperationId);
            Assert.NotNull(endpoint.ResponseInfos[fakeResponseModel.StatusCode]);
            Assert.Equal(fakeResponseModel.Description, endpoint.ResponseInfos[fakeResponseModel.StatusCode].Description);
            Assert.Contains(nameof(FakeResponseModel), endpoint.ResponseInfos[fakeResponseModel.StatusCode].Schema.Ref);
        }

        [Fact]
        public void Endpoint_with_default_response_model()
        {
            //Arrange
            var fakeEndpoint = new FakeEndpoint();
            var fakeResponseModel = new FakeResponseModel();

            //Act
            var endpoint = new Endpoint(fakeEndpoint.OperationName)
                .WithDefaultResponse(typeof(FakeResponseModel), fakeResponseModel.Description);

            //Assert
            Assert.Equal(fakeEndpoint.OperationName, endpoint.OperationId);
            Assert.NotNull(endpoint.ResponseInfos[fakeResponseModel.StatusCode]);
            Assert.Equal(fakeResponseModel.Description, endpoint.ResponseInfos[fakeResponseModel.StatusCode].Description);
            Assert.Contains(nameof(FakeResponseModel), endpoint.ResponseInfos[fakeResponseModel.StatusCode].Schema.Ref);
        }

        [Fact]
        public void Endpoint_with_response()
        {
            //Arrange
            var fakeEndpoint = new FakeEndpoint();
            var fakeResponse = new FakeResponse();

            //Act
            var endpoint = new Endpoint(fakeEndpoint.OperationName)
                .WithResponse(fakeResponse.StatusCode, fakeResponse.Description);

            //Assert
            Assert.Equal(fakeEndpoint.OperationName, endpoint.OperationId);
            Assert.NotNull(endpoint.ResponseInfos[fakeResponse.StatusCode]);
            Assert.Equal(fakeResponse.Description, endpoint.ResponseInfos[fakeResponse.StatusCode].Description);
        }
    }
}