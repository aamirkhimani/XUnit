using System;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewAppTests.Repository
{
	public class PokemonRepositoryTests
	{

		private async Task<DataContext> GetDatabaseContext()
		{
			var options = new DbContextOptionsBuilder<DataContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;

			var dataContext = new DataContext(options);
			await dataContext.Database.EnsureCreatedAsync();

			if(await dataContext.Pokemon.CountAsync() <= 0)
			{
				for(int i=1; i<10; i++)
				{
					dataContext.Pokemon.Add(new Pokemon()
					{
						Id = i,
						Name = "Pikachu",
						BirthDate = new DateTime(1903, 1, 1),
						PokemonCategories = new List<PokemonCategory>()
							{
								new PokemonCategory { Category = new Category() { Name = "Electric"}}
							},
						Reviews = new List<Review>()
							{
								new Review { Title="Pikachu",Text = "Pickahu is the best pokemon, because it is electric", Rating = 5,
								Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
								new Review { Title="Pikachu", Text = "Pickachu is the best a killing rocks", Rating = 5,
								Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
								new Review { Title="Pikachu",Text = "Pickchu, pickachu, pikachu", Rating = 1,
								Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
							}
					});

					await dataContext.SaveChangesAsync();
				}
			}

			return dataContext;
		}

		[Fact]
		public async void PokemonRepository_GetPokemn_ReturnsPokemon()
		{
			//Arrange
			var pokemonName = "Pikachu";
			var dbContext = await GetDatabaseContext();
			var pokemonRepository = new PokemonRepository(dbContext);


			//Act
			var result = pokemonRepository.GetPokemon(pokemonName);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(Pokemon));
			result.BirthDate.Should().NotHaveYear(1993);
		}

		[Fact]
		public async void PokemonRepository_GetPokemonRating_ReturnsDecimalBetweenOneAndTen()
		{
			//Arrange
			var pokeId = 1;
			var dbContext = await GetDatabaseContext();
			var pokemonRepository = new PokemonRepository(dbContext);

			//Act
			var result = pokemonRepository.GetPokemonRating(pokeId);

			//Assert
			result.Should().NotBe(0);
			result.Should().BeInRange(1, 5);
		}
	}
}