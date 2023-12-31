﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tienda.Infraestructure.Data;

#nullable disable

namespace Tienda.Infraestructure.Migrations
{
    [DbContext(typeof(TiendaDbContext))]
    [Migration("20230505110127_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Tienda.Domain.AggregatesModel.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar")
                        .HasColumnName("telefono")
                        .HasColumnOrder(2);

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<int>("_clienteTypeId")
                        .HasColumnType("int")
                        .HasColumnName("clienteType")
                        .HasColumnOrder(8);

                    b.HasKey("Id");

                    b.HasIndex("_clienteTypeId");

                    b.ToTable("Clientes", "tienda");
                });

            modelBuilder.Entity("Tienda.Domain.AggregatesModel.ClienteType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.ToTable("ClienteTypes", "tienda");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "premium"
                        },
                        new
                        {
                            Id = 2,
                            Name = "standard"
                        });
                });

            modelBuilder.Entity("Tienda.Domain.AggregatesModel.LineaPedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("PedidoId")
                        .HasColumnType("int")
                        .HasColumnOrder(4);

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<int>("_cantidad")
                        .HasColumnType("int")
                        .HasColumnName("Cantidad")
                        .HasColumnOrder(3);

                    b.Property<int>("_productoId")
                        .HasColumnType("int")
                        .HasColumnName("ProductoId")
                        .HasColumnOrder(2);

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.HasIndex("_productoId");

                    b.ToTable("LineaPedidos", "tienda");
                });

            modelBuilder.Entity("Tienda.Domain.AggregatesModel.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime")
                        .HasColumnName("fecha")
                        .HasColumnOrder(2);

                    b.Property<double>("Total")
                        .HasColumnType("float")
                        .HasColumnName("total")
                        .HasColumnOrder(3);

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<int>("_clienteId")
                        .HasColumnType("int")
                        .HasColumnName("ClienteId")
                        .HasColumnOrder(4);

                    b.HasKey("Id");

                    b.HasIndex("_clienteId");

                    b.ToTable("Pedidos", "tienda");
                });

            modelBuilder.Entity("Tienda.Domain.AggregatesModel.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("nombre")
                        .HasColumnOrder(3);

                    b.Property<double>("Precio")
                        .HasColumnType("float")
                        .HasColumnName("precio")
                        .HasColumnOrder(2);

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Productos", "tienda");
                });

            modelBuilder.Entity("Tienda.Domain.AggregatesModel.Cliente", b =>
                {
                    b.HasOne("Tienda.Domain.AggregatesModel.ClienteType", "ClienteType")
                        .WithMany()
                        .HasForeignKey("_clienteTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("Tienda.Domain.AggregatesModel.Direccion", "Direccion", b1 =>
                        {
                            b1.Property<int>("ClienteId")
                                .HasColumnType("int");

                            b1.Property<string>("Calle")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar")
                                .HasColumnOrder(3);

                            b1.Property<string>("Ciudad")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar")
                                .HasColumnOrder(4);

                            b1.Property<string>("CodigoPostal")
                                .IsRequired()
                                .HasMaxLength(5)
                                .HasColumnType("varchar")
                                .HasColumnOrder(7);

                            b1.Property<string>("Pais")
                                .IsRequired()
                                .HasMaxLength(35)
                                .HasColumnType("varchar")
                                .HasColumnOrder(6);

                            b1.Property<string>("Provincia")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar")
                                .HasColumnOrder(5);

                            b1.HasKey("ClienteId");

                            b1.ToTable("Clientes", "tienda");

                            b1.WithOwner()
                                .HasForeignKey("ClienteId");
                        });

                    b.Navigation("ClienteType");

                    b.Navigation("Direccion")
                        .IsRequired();
                });

            modelBuilder.Entity("Tienda.Domain.AggregatesModel.LineaPedido", b =>
                {
                    b.HasOne("Tienda.Domain.AggregatesModel.Pedido", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tienda.Domain.AggregatesModel.Producto", null)
                        .WithMany()
                        .HasForeignKey("_productoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Tienda.Domain.AggregatesModel.Pedido", b =>
                {
                    b.HasOne("Tienda.Domain.AggregatesModel.Cliente", null)
                        .WithMany()
                        .HasForeignKey("_clienteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Tienda.Domain.AggregatesModel.Pedido", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
