using Microsoft.EntityFrameworkCore.Migrations;

namespace APICatalogo.Migrations
{
    public partial class populadb : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Categories (Name, ImageUrl) Values('Drinks','http://www.macoratti.net/Imagens/1.jpg')");

            mb.Sql("Insert into Categories (Name, ImageUrl) Values('Snacks','http://www.macoratti.net/Imagens/2.jpg')");

            mb.Sql("Insert into Categories (Name, ImageUrl) Values('Desserts','http://www.macoratti.net/Imagens/3.jpg')");



            mb.Sql("Insert into Products (Name,Description,Price,ImageUrl,Stock,RegistrationDate,CategoryId)" +

              "Values('Coca-cola Diet', 'Soda 350 ml', 5.45,'http://www.macoratti.net/Imagens/coca.jpg',50, now(),(Select Id from Categories where Name='Drinks'))");



            mb.Sql("Insert into Products (Name,Description,Price,ImageUrl,Stock,RegistrationDate,CategoryId)" +

                "Values('Tuna Snack','Tuna sandwich with tomato sauce',8.50,'http://www.macoratti.net/Imagens/atum.jpg',10, now(),(Select Id from Categories where Name='Snacks'))");



            mb.Sql("Insert into Products (Name,Description,Price,ImageUrl,Stock,RegistrationDate,CategoryId)" +

                 "Values('Pudding', 'Condensed milk pudding 100g',6.75,'http://www.macoratti.net/Imagens/pudim.jpg',20, now(), (Select Id from Categories where Name='Desserts'))");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categories");

            mb.Sql("Delete from Products");
        }
    }
}
