using LojaDeDiversidades.Api.Controllers.V1;
using LojaDeDiversidades.Application.DTOs;
using LojaDeDiversidades.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LojaDeDiversidades.Tests
{
    public class UsuarioControllerTests
    {
        private readonly Mock<IUsuarioService> _usuarioServiceMock;
        private readonly UsuarioController _controller;

        public UsuarioControllerTests()
        {
            _usuarioServiceMock = new Mock<IUsuarioService>();
            _controller = new UsuarioController(_usuarioServiceMock.Object);
        }

        [Fact]
        public async Task ListarTodos_ReturnsOkWithUsuarios()
        {
            // Arrange
            var usuarios = new List<UsuarioDto>
            {
                new UsuarioDto { Id = 1, Nome = "User1", Email = "user1@email.com" }
            };
            _usuarioServiceMock.Setup(s => s.ListarTodosAsync()).ReturnsAsync(usuarios);

            // Act
            var result = await _controller.ListarTodos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(usuarios, okResult.Value);
        }

        [Fact]
        public async Task ObterPorId_ReturnsOk_WhenUsuarioExists()
        {
            // Arrange
            var usuario = new UsuarioDto { Id = 1, Nome = "User1", Email = "user1@email.com" };
            _usuarioServiceMock.Setup(s => s.ObterPorIdAsync(1)).ReturnsAsync(usuario);

            // Act
            var result = await _controller.ObterPorId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(usuario, okResult.Value);
        }

        [Fact]
        public async Task ObterPorId_ReturnsNotFound_WhenUsuarioDoesNotExist()
        {
            // Arrange
            _usuarioServiceMock.Setup(s => s.ObterPorIdAsync(1)).ReturnsAsync((UsuarioDto)null);

            // Act
            var result = await _controller.ObterPorId(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Criar_ReturnsCreatedAtAction_WithUsuario()
        {
            // Arrange
            var criarDto = new CriarUsuarioDto { Nome = "User1", Email = "user1@email.com", Senha = "123", DataNascimento = DateTime.Now, Telefone = "123456" };
            var usuario = new UsuarioDto { Id = 1, Nome = "User1", Email = "user1@email.com" };
            _usuarioServiceMock.Setup(s => s.CriarAsync(criarDto)).ReturnsAsync(usuario);

            // Act
            var result = await _controller.Criar(criarDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(usuario, createdResult.Value);
            Assert.Equal(nameof(_controller.ObterPorId), createdResult.ActionName);
            Assert.Equal(usuario.Id, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task Atualizar_ReturnsNoContent_WhenIdsMatch()
        {
            // Arrange
            var usuarioDto = new UsuarioDto { Id = 1, Nome = "User1", Email = "user1@email.com" };

            // Act
            var result = await _controller.Atualizar(1, usuarioDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _usuarioServiceMock.Verify(s => s.AtualizarAsync(usuarioDto), Times.Once);
        }

        [Fact]
        public async Task Atualizar_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var usuarioDto = new UsuarioDto { Id = 2, Nome = "User1", Email = "user1@email.com" };

            // Act
            var result = await _controller.Atualizar(1, usuarioDto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Id não bate.", badRequest.Value);
            _usuarioServiceMock.Verify(s => s.AtualizarAsync(It.IsAny<UsuarioDto>()), Times.Never);
        }

        [Fact]
        public async Task Remover_ReturnsNoContent()
        {
            // Arrange
            var id = 1;

            // Act
            var result = await _controller.Remover(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _usuarioServiceMock.Verify(s => s.RemoverAsync(id), Times.Once);
        }
    }
}
