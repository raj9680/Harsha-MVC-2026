using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PersonsDbContext: DbContext
    {
        public PersonsDbContext(DbContextOptions options): base(options)
        {
            
        }


        public DbSet<Country> Countries { get; set; }
        public DbSet<Person> Persons { get; set; }



        // Bind above tables/DbSet to a DB
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring Table Name
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Person>().ToTable("Persons");


            //Next Register DBSet as Service in Program.cs


            // Seed to Countries
            string countriesJson = System.IO.File.ReadAllText("countries.json");
            List<Country> countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(countriesJson);

            foreach (Country country in countries)
                modelBuilder.Entity<Country>().HasData(country);


            // Seed to Person
            string personsJson = System.IO.File.ReadAllText("persons.json");
            List<Person> persons = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(personsJson);

            foreach (Person person in persons)
                modelBuilder.Entity<Person>().HasData(person);

            // Fluent API - 1
            //modelBuilder.Entity<Person>().Property(temp => temp.TIN)
            //    .HasColumnName("TaxIdentificationNumber")
            //    .HasColumnType("varchar(8)")
            //    .HasDefaultValue("ABC12345");

            //Fluent API - 2
            // Adds database index for the specified column for faster searches
            // modelBuilder.Entity<Person>().HasIndex(temp => temp.TIN).IsUnique();

            // Adds check constraints for the specified column - that executes for insert & update
            //modelBuilder.Entity<Person>().HasCheckConstraint("CHK_TIN", "len([TaxIdentificationNumber]) = 8");

            // Fluent API - 3 (Table Relations)
            //modelBuilder.Entity<Person>(entity =>
            //{
            //    entity.HasOne<Country>(c => c.Country)
            //    .WithMany(p => p.Persons)
            //    .HasForeignKey(p => p.CountryID);
            //});

        }


        // SP to Get All Persons
        public List<Person> sp_GetAllPersons()
        {
            return Persons.FromSqlRaw("Execute [dbo].[GetAllPersons]").ToList();
        }

        // SP to Insert
        public int sp_InsertPerson(Person person)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@PersonID", person.PersonID),
                new SqlParameter("@PersonName", person.PersonName),
                new SqlParameter("@Email", person.Email),
                new SqlParameter("@DateOfBirth", person.DateOfBirth),
                new SqlParameter("@Gender", person.Gender),
                new SqlParameter("@CountryID", person.CountryID),
                new SqlParameter("@Address", person.Address),
                new SqlParameter("@ReceiveNewsLetter", person.ReceiveNewsLetter)
            };

            return Database.ExecuteSqlRaw("EXECUTE [dbo].[InsertPerson] @PersonID, @PersonName, @Email, @DateOfBirth, @Gender, @CountryID, @Address, @ReceiveNewsLetter", parameters);
        }
    }
}
