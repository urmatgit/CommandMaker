using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Domain.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Org.BouncyCastle.Asn1.BC;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;
public class PlayerConfig : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(b => b.Name)
                .HasMaxLength(256);
        builder.Property(b => b.Age)
            .IsRequired()
            .HasMaxLength(2);
        
            



    }
}
public class TeamConfig : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(b => b.Name)
                .HasMaxLength(256);
        builder
            .Property(b => b.Captain)
                .HasMaxLength(256);
    }
}
public class GameConfig : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(b => b.Name)
                .HasMaxLength(256);
        builder
            .Property(b => b.DateTime)
            .IsRequired();
    }
}

//public class BrandConfig : IEntityTypeConfiguration<Brand>
//{
//    public void Configure(EntityTypeBuilder<Brand> builder)
//    {
//        builder.IsMultiTenant();

//        builder
//            .Property(b => b.Name)
//                .HasMaxLength(256);
//    }
//}

//public class ProductConfig : IEntityTypeConfiguration<Product>
//{
//    public void Configure(EntityTypeBuilder<Product> builder)
//    {
//        builder.IsMultiTenant();

//        builder
//            .Property(b => b.Name)
//                .HasMaxLength(1024);

//        builder
//            .Property(p => p.ImagePath)
//                .HasMaxLength(2048);
//    }
//}