﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using ProjectCore.Infrastructure;
using System;

namespace ProjectCore.Infrastructure.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20180515120709_Mig4")]
    partial class Mig4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectCore.Domain.Model.DomainModel.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("CustomerAge");

                    b.Property<string>("CustomerName");

                    b.Property<string>("CustomerPhone");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.DomainModel.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<Guid>("CustomrId");

                    b.Property<string>("OrderCreateDate");

                    b.Property<decimal>("OrderTotlePrice");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.DomainModel.OrderDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<decimal>("OrderDetailPrice");

                    b.Property<Guid>("OrderId");

                    b.Property<int>("OrderNumBer");

                    b.Property<Guid>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.DomainModel.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("ProductColor");

                    b.Property<string>("ProductName");

                    b.Property<int>("ProductPrice");

                    b.Property<int>("ProductStock");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.DomainModel.ShoppingCart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("CartTotalPrice");

                    b.Property<string>("Code");

                    b.Property<Guid>("CustomerId");

                    b.HasKey("Id");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.DomainModel.ShoppingCartDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("CartDetailPrice");

                    b.Property<string>("Code");

                    b.Property<Guid>("ProductId");

                    b.Property<string>("ShopProductColor");

                    b.Property<string>("ShopProductName");

                    b.Property<int>("ShopProductNumBer");

                    b.Property<double>("ShopProductPrice");

                    b.Property<Guid?>("ShoppingCartId");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("ShoppingCartDetails");
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.DomainModel.Customer", b =>
                {
                    b.OwnsOne("ProjectCore.Domain.Model.DomainValueObject.CustomerAddress", "CustomerAddr", b1 =>
                        {
                            b1.Property<Guid>("CustomerId");

                            b1.Property<string>("AddrCity");

                            b1.Property<string>("AddrCounty");

                            b1.Property<string>("AddrProvince");

                            b1.Property<string>("Address");

                            b1.ToTable("Customers");

                            b1.HasOne("ProjectCore.Domain.Model.DomainModel.Customer")
                                .WithOne("CustomerAddr")
                                .HasForeignKey("ProjectCore.Domain.Model.DomainValueObject.CustomerAddress", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.DomainModel.OrderDetails", b =>
                {
                    b.HasOne("ProjectCore.Domain.Model.DomainModel.Order", "OrderInfo")
                        .WithMany("OrderDetailList")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.DomainModel.ShoppingCartDetails", b =>
                {
                    b.HasOne("ProjectCore.Domain.Model.DomainModel.ShoppingCart", "ShoppingCart")
                        .WithMany("ShoppingCartDetailList")
                        .HasForeignKey("ShoppingCartId");
                });
#pragma warning restore 612, 618
        }
    }
}
