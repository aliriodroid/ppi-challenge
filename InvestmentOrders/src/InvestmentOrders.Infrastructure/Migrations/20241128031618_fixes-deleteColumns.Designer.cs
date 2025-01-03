﻿// <auto-generated />
using InvestmentOrders.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InvestmentOrders.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241128031618_fixes-deleteColumns")]
    partial class fixesdeleteColumns
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("InvestmentOrders.Domain.Entities.Activo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("numeric");

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TipoActivoId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TipoActivoId");

                    b.ToTable("Activos");
                });

            modelBuilder.Entity("InvestmentOrders.Domain.Entities.EstadoOrden", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DescripcionEstado")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EstadosOrden");
                });

            modelBuilder.Entity("InvestmentOrders.Domain.Entities.Orden", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivoId")
                        .HasColumnType("integer");

                    b.Property<int>("Cantidad")
                        .HasColumnType("integer");

                    b.Property<int>("EstadoOrdenId")
                        .HasColumnType("integer");

                    b.Property<int>("IdCuenta")
                        .HasColumnType("integer");

                    b.Property<decimal>("MontoTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("NombreActivo")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<char>("Operacion")
                        .HasColumnType("character(1)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ActivoId");

                    b.HasIndex("EstadoOrdenId");

                    b.ToTable("Ordenes");
                });

            modelBuilder.Entity("InvestmentOrders.Domain.Entities.TipoActivo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TiposActivo");
                });

            modelBuilder.Entity("InvestmentOrders.Domain.Entities.Activo", b =>
                {
                    b.HasOne("InvestmentOrders.Domain.Entities.TipoActivo", "TipoActivo")
                        .WithMany()
                        .HasForeignKey("TipoActivoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoActivo");
                });

            modelBuilder.Entity("InvestmentOrders.Domain.Entities.Orden", b =>
                {
                    b.HasOne("InvestmentOrders.Domain.Entities.Activo", "Activo")
                        .WithMany()
                        .HasForeignKey("ActivoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvestmentOrders.Domain.Entities.EstadoOrden", "EstadoOrden")
                        .WithMany("Ordenes")
                        .HasForeignKey("EstadoOrdenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activo");

                    b.Navigation("EstadoOrden");
                });

            modelBuilder.Entity("InvestmentOrders.Domain.Entities.EstadoOrden", b =>
                {
                    b.Navigation("Ordenes");
                });
#pragma warning restore 612, 618
        }
    }
}
