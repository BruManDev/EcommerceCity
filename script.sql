CREATE DATABASE bdEcommerce;

USE bdEcommerce;

CREATE TABLE Usuario(
	Id int primary key auto_increment,
    Nome varchar(50) not null,
    Email varchar(50) not null,
    Senha varchar(50) not null
);

CREATE TABLE Cliente(
	CodCli int primary key auto_increment,
    NomeCli varchar(50) not null,
    TelCli varchar(20) not null,
    EmailCli varchar(50) not null
);

CREATE TABLE Produto(
	CodProd int primary key auto_increment,
    Nome varchar(50) not null,
    Descricao varchar(100) not null,
    Quantidade int not null,
    Preco decimal (10,2) not null
);

insert into Usuario (Nome, Email, Senha) values ('Bruno', 'bruno123@gmail.com', 2393);

SELECT * FROM Produto;
SELECT * FROM Usuario;
SELECT * FROM Cliente;