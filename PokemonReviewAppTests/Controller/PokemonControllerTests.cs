using System;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Controllers;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewAppTests.Controller
{
	public class PokemonControllerTests
	{
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly PokemonController _pokemonController;

        public PokemonControllerTests()
		{
			_pokemonRepository = A.Fake<IPokemonRepository>();
			_reviewRepository = A.Fake<IReviewRepository>();
			_mapper = A.Fake<IMapper>();
			_pokemonController = new PokemonController(_pokemonRepository, _reviewRepository, _mapper);
		}

		[Fact]
		public void PokemonController_GetPokemon_ReturnSuccess()
		{
			//Arrange
			var pokemons = A.Fake<ICollection<Pokemon>>();
			var pokemonList = A.Fake<List<PokemonDto>>();
			A.CallTo(() => _mapper.Map<List<PokemonDto>>(pokemons)).Returns(pokemonList);
			var controller = new PokemonController(_pokemonRepository, _reviewRepository, _mapper);

			//Act
			var result = controller.GetPokemons();

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

		[Fact]
		public void PokemonController_CreatePokemon_ReturnSuccess()
		{
			//Arrange
			int ownerId = 1;
			int cateId = 1;
			var pokemonCreate = A.Fake<PokemonDto>();
			var pokemonMap = A.Fake<Pokemon>();
			var GetPokemonTrimToUpperResult = A.Fake<Pokemon>();
			A.CallTo(() => _pokemonRepository.GetPokemonTrimToUpper(pokemonCreate)).Returns(null);
			A.CallTo(() => _mapper.Map<Pokemon>(pokemonCreate)).Returns(pokemonMap);
			A.CallTo(() => _pokemonRepository.CreatePokemon(ownerId, cateId, pokemonMap)).Returns(true);


			//Act
			var result = _pokemonController.CreatePokemon(ownerId, cateId, pokemonCreate);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void PokemonController_CreatePokemon_ReturnOwnerAlreadyExists()
        {
            //Arrange
            int ownerId = 1;
            int cateId = 1;
            var pokemonCreate = A.Fake<PokemonDto>();
            var pokemonMap = A.Fake<Pokemon>();
            var GetPokemonTrimToUpperResult = A.Fake<Pokemon>();
            A.CallTo(() => _pokemonRepository.GetPokemonTrimToUpper(pokemonCreate)).Returns(GetPokemonTrimToUpperResult);
            A.CallTo(() => _mapper.Map<Pokemon>(pokemonCreate)).Returns(pokemonMap);
            A.CallTo(() => _pokemonRepository.CreatePokemon(ownerId, cateId, pokemonMap)).Returns(true);


            //Act
            var result = _pokemonController.CreatePokemon(ownerId, cateId, pokemonCreate);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ObjectResult));
        }
    }
}

