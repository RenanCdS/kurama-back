using Kurama.CrossCutting.Options;
using Kurama.Domain.Common;
using Kurama.Domain.DTOs;
using Kurama.Domain.Interfaces.Data;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kurama.Data.Facades
{
    public class DockerFacade : IDockerFacade
    {
        private readonly DockerConfigOptions _dockerConfig;
        private readonly HttpClient _httpClient;
        public DockerFacade(IOptions<DockerConfigOptions> options, HttpClient httpClient)
        {
            _dockerConfig = options.Value;
            _httpClient = httpClient;
        }

        public async Task<Response<bool>> CreateContainerAsync(CreateContainerDto createContainerDto)
        {
            var bodyString = JsonSerializer.Serialize(createContainerDto, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            var body = new StringContent(bodyString, Encoding.UTF8, mediaType: "application/json");
            var createContainerResponse = await _httpClient.PostAsync($"{_dockerConfig.DockerHost}/containers/create", body);
            var createContainerResponseString = await createContainerResponse.Content.ReadAsStringAsync();
            var containerCreateResponseDto = JsonSerializer.Deserialize<CreateContainerResponseDto>(createContainerResponseString);

            if (createContainerResponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                await StartContainer(containerCreateResponseDto.Id);
                return Response<bool>.Ok();
            }

            return Response<bool>.Fail(containerCreateResponseDto.Message);
        }

        private async Task<Response<bool>> StartContainer(string id)
        {
            var createContainerResponse = await _httpClient.PostAsync($"{_dockerConfig.DockerHost}/containers/{id}/start", null);

            if (!createContainerResponse.IsSuccessStatusCode)
            {
                return Response<bool>.Fail();
            }

            return Response<bool>.Ok();
        }

        public async Task<IEnumerable<ContainerDto>> GetContainersAsync()
        {
            var getContainersResponse = await _httpClient.GetAsync($"{_dockerConfig.DockerHost}/containers/json?all=true");

            var getContainersResponseBody = await getContainersResponse.Content.ReadAsStringAsync();

            var containers = JsonSerializer.Deserialize<IEnumerable<ContainerDto>>(getContainersResponseBody);
            return containers;
        }

        public async Task<Response<bool>> DeleteAllContainersAsync()
        {
            var allContainers = await GetContainersAsync();

            if (allContainers.Count() > 0)
            {
                await StopContainers(allContainers);
            }

            var deleteStoppedContainersResponse = await _httpClient.PostAsync($"{_dockerConfig.DockerHost}/containers/prune", null);

            if (!deleteStoppedContainersResponse.IsSuccessStatusCode)
            {
                return Response<bool>.Fail();
            }
            return Response<bool>.Ok();
        }

        private async Task<Response<bool>> StopContainers(IEnumerable<ContainerDto> allContainers)
        {
            foreach (var container in allContainers)
            {
                var stopContainerResponse = await StopContainer(container.Id);
                if (!stopContainerResponse.IsSuccess)
                {
                    return Response<bool>.Fail();
                }
            }

            return Response<bool>.Ok();
        }

        private async Task<Response<bool>> StopContainer(string containerId)
        {
            var stopContainerResponse = await _httpClient.PostAsync($"{_dockerConfig.DockerHost}/containers/{containerId}/stop", null);

            if (stopContainerResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return Response<bool>.Fail("Container não encontrado");
            }

            if (stopContainerResponse.StatusCode >= System.Net.HttpStatusCode.BadRequest)
            {
                return Response<bool>.Fail();
            }

            return Response<bool>.Ok();
        }

        public async Task<Response<bool>> DeleteContainerByIdAsync(string containerId)
        {
            var stopContainerResponse = await StopContainer(containerId);

            if (!stopContainerResponse.IsSuccess)
                return stopContainerResponse;

            var deleteStoppedContainerResponse = await _httpClient.DeleteAsync($"{_dockerConfig.DockerHost}/containers/{containerId}");

            if (!deleteStoppedContainerResponse.IsSuccessStatusCode)
            {
                return Response<bool>.Fail();
            }
            return Response<bool>.Ok();
        }

        public async Task<Response<bool>> PullImageAsync(string imageName)
        {
            var pullImageResponse = await _httpClient.PostAsync($"{_dockerConfig.DockerHost}/images/create?fromImage={imageName}&tag=latest", null);

            if (!pullImageResponse.IsSuccessStatusCode)
            {
                var pullImageResponseString = await pullImageResponse.Content.ReadAsStringAsync();
                var pullImageResponseDto = JsonSerializer.Deserialize<PullImageResponseDto>(pullImageResponseString, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return Response<bool>.Fail(pullImageResponseDto.Message);
            }

            return Response<bool>.Ok();
        }

        public async Task<Response<IEnumerable<ImageDto>>> GetAllImagesAsync()
        {
            var getImagesResponse = await _httpClient.GetAsync($"{_dockerConfig.DockerHost}/images/json");

            var getImagesResponseBody = await getImagesResponse.Content.ReadAsStringAsync();

            var images = JsonSerializer.Deserialize<IEnumerable<ImageDto>>(getImagesResponseBody);
            return Response<IEnumerable<ImageDto>>.Ok(images);
        }

        public async Task<Response<bool>> DeleteAllImagesAsync()
        {
            var allImagesResponse = await GetAllImagesAsync();
            if (!allImagesResponse.IsSuccess)
            {
                return Response<bool>.Fail();
            }
            var allImages = allImagesResponse.Value;
            var deletedImages = new List<string>();

            foreach (var image in allImages)
            {
                var deleteImageResponse = await DeleteImage(image.Id);
                if (deleteImageResponse.IsSuccess)
                {
                    deletedImages.Add(image.Id);
                }
            }

            return Response<bool>.Ok();
        }

        private async Task<Response<bool>> DeleteImage(string imageId)
        {
            var deleteImageResponse = await _httpClient.DeleteAsync($"{_dockerConfig.DockerHost}/images/{imageId}?force=true");

            if (!deleteImageResponse.IsSuccessStatusCode)
            {
                return Response<bool>.Fail();
            }

            return Response<bool>.Ok();
        }

        public async Task<Response<bool>> DeleteImageByIdAsync(string imageId)
        {
            var deleteImageByIdResponse = await _httpClient.DeleteAsync($"{_dockerConfig.DockerHost}/images/{imageId}");

            if (!deleteImageByIdResponse.IsSuccessStatusCode)
            {
                return Response<bool>.Fail();
            }
            return Response<bool>.Ok();
        }
    }
}
