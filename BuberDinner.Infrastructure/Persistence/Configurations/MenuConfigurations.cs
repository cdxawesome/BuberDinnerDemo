using BuberDinner.Domain.HostAggregate.ValueObjects;
using BuberDinner.Domain.MenuAggregate;
using BuberDinner.Domain.MenuAggregate.Entities;
using BuberDinner.Domain.MenuAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuberDinner.Infrastructure.Persistence.Configurations;

public class MenuConfigurations : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        ConfigureMenusTable(builder);
        ConfigureMenuSectionsTable(builder);
    }

    private void ConfigureMenuSectionsTable(EntityTypeBuilder<Menu> builder)
    {
        // 配置 Menu 和 MenuSection 的关系
        builder.OwnsMany(m => m.Sections, section =>
        {
            section.ToTable("MenuSections");
            // 配置 MenuSection 的外键
            section.WithOwner().HasForeignKey("MenuId");

            // 配置 MenuSection 的主键为一个联合主键，结合 MenuId 和 Id
            section.HasKey(new[]{"Id","MenuId"});

            section.Property(s=>s.Id)
            .HasColumnName("MenuSetionId")
            .ValueGeneratedNever()
            .HasConversion(
                id=>id.Value,
                value=>MenuSectionId.Create(value));

            section.Property(s=>s.Name)
            .HasMaxLength(100);

            section.Property(s=>s.Description)
            .HasMaxLength(100);

            section.OwnsMany(s=>s.Items,ib=>{
                ib.ToTable("MenuItems");
                ib.HasKey(nameof(MenuItem.Id),"MenuSectionId","MenuId");
                ib.WithOwner().HasForeignKey("MenuSectionId","MenuId");
                ib.Property(i=>i.Id)
                .HasColumnName("MenuItemId")
                .ValueGeneratedNever()
                .HasConversion(
                    id=>id.Value,
                    value=>MenuItemId.Create(value));
                ib.Property(i=>i.Name)
                .HasMaxLength(100);
                ib.Property(i=>i.Description)
                .HasMaxLength(100);

            });

            section.Navigation(s=>s.Items).Metadata.SetField("_items");
            section.Navigation(s=>s.Items).Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);
        });
        /*
        这里配置EFCore的导航访问模式权限。在Menu类中，Sections属性是一个只读的集合
        ，所以我们需要将其配置为字段访问模式,这样EFCore才能访问到Sections属性。
        */
        builder.Metadata.FindNavigation(nameof(Menu.Sections))!
        .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureMenusTable(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("Menus");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id)
        .ValueGeneratedNever()
        .HasConversion(
            id => id.Value,
            value => MenuId.Create(value));

        builder.Property(m => m.Name)
        .HasMaxLength(100);
        builder.Property(m => m.Description)
        .HasMaxLength(100);
        builder.Property(m => m.HostId)
        .HasConversion(
            id => id.Value,
            value => HostId.Create(value));
    }
}