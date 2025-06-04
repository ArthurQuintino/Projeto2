create database dbprojeto2;
use dbprojeto2;

create table Usuarios(
	idUser int,
    Nome varchar(100),
    Email varchar(100),
    Senha varchar(20)
);

create table Produtos(
	idProd int, 
    Nome varchar(100),
    Descricao varchar(100),
    Preco decimal,
    Quantidade int
)