// <copyright file="DataBaseContext.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data
{
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// DataBase Context
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public partial class DataBaseContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataBaseContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public DbSet<CategoryModel> Categories { get; set; }

        /// <summary>
        /// Gets or sets the user details.
        /// </summary>
        /// <value>
        /// The user details.
        /// </value>
        public DbSet<UserDetailModel> UserDetails { get; set; }

        /// <summary>
        /// Gets or sets the packages.
        /// </summary>
        /// <value>
        /// The packages.
        /// </value>
        public DbSet<PackageModel> Packages { get; set; }

        /// <summary>
        /// Gets or sets the package image.
        /// </summary>
        /// <value>
        /// The package image.
        /// </value>
        public DbSet<PackageImageModel> PackageImages { get; set; }

        /// <summary>
        /// Gets or sets the type of the plan.
        /// </summary>
        /// <value>
        /// The type of the plan.
        /// </value>
        public DbSet<PlanTypeModel> PlanType { get; set; }

        /// <summary>
        /// Gets or sets the hotel price.
        /// </summary>
        /// <value>
        /// The hotel price.
        /// </value>
        public DbSet<HotelPriceModel> HotelPrice { get; set; }

        /// <summary>
        /// Gets or sets the hotel price detail.
        /// </summary>
        /// <value>
        /// The hotel price detail.
        /// </value>
        public DbSet<HotelPriceDetailModel> HotelPriceDetail { get; set; }

        /// <summary>
        /// Gets or sets the type of the plan.
        /// </summary>
        /// <value>
        /// The type of the plan.
        /// </value>
        public DbSet<RoomTypeModel> RoomType { get; set; }

        /// <summary>
        /// Gets or sets the hotel.
        /// </summary>
        /// <value>
        /// The hotel.
        /// </value>
        public DbSet<HotelModel> Hotel { get; set; }

        /// <summary>
        /// Gets or sets the accommodation.
        /// </summary>
        /// <value>
        /// The accommodation.
        /// </value>
        public DbSet<AccommodationModel> Accommodation { get; set; }

        /// <summary>
        /// Gets or sets the type of the plan.
        /// </summary>
        /// <value>
        /// The type of the plan.
        /// </value>
        public DbSet<PackageAreaModel> PackageArea { get; set; }

        /////// <summary>
        /////// Gets or sets the type of the plan.
        /////// </summary>
        /////// <value>
        /////// The type of the plan.
        /////// </value>
        ////public DbSet<UserCountriesModel> UserCountries { get; set; }

        /// <summary>
        /// Gets or sets the application user.
        /// </summary>
        /// <value>
        /// The application user.
        /// </value>
        public DbSet<ApplicationUserModel> ApplicationUser { get; set; }

        /// <summary>
        /// Gets or sets the hotel terms.
        /// </summary>
        /// <value>
        /// The hotel terms.
        /// </value>
        public DbSet<HotelTermModel> HotelTerms { get; set; }

        /// <summary>
        /// Gets or sets the hotel validity.
        /// </summary>
        /// <value>
        /// The hotel validity.
        /// </value>
        public DbSet<HotelValidityModel> HotelValidity { get; set; }

        /////// <summary>
        /////// Gets or sets the hotel room night target.
        /////// </summary>
        /////// <value>
        /////// The hotel room night target.
        /////// </value>
        ////public DbSet<HotelRoomNightTargetModel> HotelRoomNightTarget { get; set; }

        /// <summary>
        /// Gets or sets the package reminder.
        /// </summary>
        /// <value>
        /// The package reminder.
        /// </value>
        public DbSet<PackageReminderModel> PackageReminder { get; set; }

        /// <summary>
        /// Gets or sets the hotel booking.
        /// </summary>
        /// <value>
        /// The hotel booking.
        /// </value>
        public DbSet<HotelBookingModel> HotelBooking { get; set; }

        /// <summary>
        /// Gets or sets the hotel booking person detail.
        /// </summary>
        /// <value>
        /// The hotel booking person detail.
        /// </value>
        public DbSet<HotelBookingPersonDetailModel> HotelBookingPersonDetail { get; set; }

        /// <summary>
        /// Gets or sets the user transaction.
        /// </summary>
        /// <value>
        /// The user transaction.
        /// </value>
        public DbSet<UserTransactionModel> UserTransaction { get; set; }

        /// <summary>
        /// Gets or sets the flight destination.
        /// </summary>
        /// <value>
        /// The flight destination.
        /// </value>
        public DbSet<FlightDestination> FlightDestination { get; set; }

        /// <summary>
        /// Gets or sets the city view.
        /// </summary>
        /// <value>
        /// The city view.
        /// </value>
        public DbSet<CityView> CityView { get; set; }

        /// <summary>
        /// Gets or sets the hotel room meal remark.
        /// </summary>
        /// <value>
        /// The hotel room meal remark.
        /// </value>
        public DbSet<HotelRoomMealRemarkModel> HotelRoomMealRemark { get; set; }

        /// <summary>
        /// Gets or sets the specific price model.
        /// </summary>
        /// <value>
        /// The specific price model.
        /// </value>
        public DbSet<SpecificPriceModel> SpecificPrice { get; set; }

        /// <summary>
        /// Gets or sets the specific price model.
        /// </summary>
        /// <value>
        /// The specific price model.
        /// </value>
        public DbSet<HomeBannerModel> HomeBanner { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public DbSet<PackageCountryModel> PackageCountry { get; set; }

        /// <summary>
        /// Gets or sets the state of the package.
        /// </summary>
        /// <value>
        /// The state of the package.
        /// </value>
        public DbSet<PackageStateModel> PackageState { get; set; }

        /// <summary>
        /// Gets or sets the package city.
        /// </summary>
        /// <value>
        /// The package city.
        /// </value>
        public DbSet<PackageCityModel> PackageCity { get; set; }

        /// <summary>
        /// Gets or sets the flight booking.
        /// </summary>
        /// <value>
        /// The flight booking.
        /// </value>
        public DbSet<FlightBookingModel> FlightBooking { get; set; }

        /// <summary>
        /// Gets or sets the hotel room type desc model.
        /// </summary>
        /// <value>
        /// The hotel room type desc model.
        /// </value>
        public DbSet<PackageHotelRoomTypeDescModel> HotelRoomTypeDesc { get; set; }

        /// <summary>
        /// Gets or sets the tour package.
        /// </summary>
        /// <value>
        /// The tour package.
        /// </value>
        public DbSet<TourPackageModel> TourPackage { get; set; }

        /// <summary>
        /// Gets or sets the tour package city.
        /// </summary>
        /// <value>
        /// The tour package city.
        /// </value>
        public DbSet<TourPackageCityModel> TourPackageCity { get; set; }

        /// <summary>
        /// Gets or sets the tour package book date.
        /// </summary>
        /// <value>
        /// The tour package book date.
        /// </value>
        public DbSet<TourPackageBookDateModel> TourPackageBookDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the package deal.
        /// </summary>
        /// <value>
        /// The type of the package deal.
        /// </value>
        public DbSet<PackageDealTypeModel> PackageDealType { get; set; }

        /// <summary>
        /// Gets or sets the tour package travel style.
        /// </summary>
        /// <value>
        /// The tour package travel style.
        /// </value>
        public DbSet<TourPackageTravelStyleModel> TourPackageTravelStyle { get; set; }

        /// <summary>
        /// Gets or sets the tour package night.
        /// </summary>
        /// <value>
        /// The tour package night.
        /// </value>
        public DbSet<TourPackageNightModel> TourPackageNights { get; set; }

        /// <summary>
        /// Gets or sets the tour package night.
        /// </summary>
        /// <value>
        /// The tour package night.
        /// </value>
        public DbSet<TourPackageNightsDepartCityModel> TourPackageNightsDepartCity { get; set; }

        /// <summary>
        /// Gets or sets the tour package night.
        /// </summary>
        /// <value>
        /// The tour package night.
        /// </value>
        public DbSet<TourPackageNightsValidityModel> TourPackageNightsValidity { get; set; }

        /// <summary>
        /// Gets or sets the tour package image.
        /// </summary>
        /// <value>
        /// The tour package image.
        /// </value>
        public DbSet<TourPackageImageModel> TourPackageImage { get; set; }

        /// <summary>
        /// Gets or sets the CompanySetting.
        /// </summary>
        /// <value>
        /// Fligh markup .
        /// </value>
        public DbSet<CompanySettingModel> CompanySetting { get; set; }

        /// <summary>
        /// Gets or sets the Currency.
        /// </summary>
        /// <value>
        /// The Currency.
        /// </value>
        public DbSet<PackageCurrencyModel> PackageCurency { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Package Relation.
        /// </summary>
        /// <value>
        /// The Vendor Package Relation.
        /// </value>
        public DbSet<Vendor_PackageModel> Vendor_Package { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Package Relation.
        /// </summary>
        /// <value>
        /// The Vendor Package Relation.
        /// </value>
        public DbSet<PackageCancellation_PackageModel> Cancellation_Package { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Package Relation.
        /// </summary>
        /// <value>
        /// The Vendor Package Relation.
        /// </value>
        public DbSet<PackagePromotionsModel> PackagePromotions { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Package Relation.
        /// </summary>
        /// <value>
        /// The Vendor Package Relation.
        /// </value>
        public DbSet<PackagePromotions_PackageModel> Package_Promotions { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Package Relation.
        /// </summary>
        /// <value>
        /// The Vendor Package Relation.
        /// </value>
        public DbSet<PackageCancellation_PackageModel> Package_Cancellation { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Package Relation.
        /// </summary>
        /// <value>
        /// The Vendor Package Relation.
        /// </value>
        public DbSet<DestinationModel> DestinationModel { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Package Relation.
        /// </summary>
        /// <value>
        /// The Vendor Package Relation.
        /// </value>
        public DbSet<DestinationValidityModel> DestinationValidityModel { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Package Relation.
        /// </summary>
        /// <value>
        /// The Vendor Package Relation.
        /// </value>
        public DbSet<ServiceTypeMasterModel> ServiceTypeModel { get; set; }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PackageTravelStyleModel>().ToTable("TravelStyle", schema: "Package");
            modelBuilder.Entity<PackageHolidayMenuModel>().ToTable("HolidayMenu", schema: "Package");
            modelBuilder.Entity<PackageHotelCategoryModel>().ToTable("HotelCategory", schema: "Package");
            modelBuilder.Entity<PackageHotelRoomTypeModel>().ToTable("HotelRoomType", schema: "Package");
            modelBuilder.Entity<StaticPageMasterModel>().ToTable("StaticPageMaster", schema: "Package");
            modelBuilder.Entity<SeoDetailModel>().ToTable("SeoDetail", schema: "Package");
            modelBuilder.Entity<DealsSeoDetail>().ToTable("SeoDetail", schema: "Deals");
            modelBuilder.Entity<PackageDealTypeModel>().ToTable("DealType", schema: "Package");
            modelBuilder.Entity<TourPackageNightModel>().ToTable("TourPackageNights", schema: "Package");
            modelBuilder.Entity<LocationDealModel>().ToTable("LocationDeal", schema: "dbo").HasKey(x => x.Id);
            modelBuilder.Entity<CompanySettingModel>(entity =>
            {
                entity.ToTable("CompanySetting", "dbo");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<PromotionTypeModel>(entity =>
            {
                entity.ToTable("PromotionType", "dbo");
                entity.HasKey(x => x.Id);
            });
            modelBuilder.Entity<PopularDestinationModel>(entity =>
            {
                entity.ToTable("PopularDestinations", "dbo");
                entity.HasKey(x => x.Id);
            });
            modelBuilder.Entity<PromotionModel>(entity =>
            {
                entity.ToTable("Promotion", "dbo");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.HasOne(x => x.MarginTypeModel).WithMany(x => x.PromotionModels).HasForeignKey(x => x.DiscountType).HasConstraintName("FK_Promotion_Margin");
            });
            modelBuilder.Entity<PackageRegionModel>(entity =>
            {
                entity.ToTable("Region", "Package");

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<PackageCountryModel>(entity =>
            {
                entity.ToTable("Country", "Package");

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SortName)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.PackageRegion)
                    .WithMany(p => p.PackageCountry)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_Country_RegionId");
            });
            modelBuilder.Entity<PackageAreaModel>(entity =>
            {
                entity.ToTable("Area", "Package");
                entity.HasOne(d => d.PackageCityModel)
                    .WithMany(p => p.PackageAreaModels)
                    .HasForeignKey(d => d.City)
                    .HasConstraintName("FK_Area_City");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PackageCityModel>(entity =>
            {
                entity.ToTable("City", "Package");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.PackageStateModel)
                   .WithMany(p => p.PackageCityModels)
                   .HasForeignKey(d => d.StateId)
                   .HasConstraintName("FK_City_State");

                entity.HasOne(d => d.PackageCountryModel)
                .WithMany(p => p.PackageCityModels)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_PackageCity_Country");
            });
            modelBuilder.Entity<PackageStateModel>(entity =>
            {
                entity.ToTable("State", "Package");

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.PackageCountryModel)
                    .WithMany(p => p.PackageStateModels)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_State_Country");
            });

            modelBuilder.Entity<PackageHotelModel>(entity =>
            {
                entity.ToTable("Hotel", "Package");

                entity.HasIndex(e => new { e.Name, e.CityId, e.Area })
                    .HasName("UNIX_Hotel_Name")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Area)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<PackageHotelRoomTypeDescModel>(entity =>
            {
                entity.ToTable("HotelRoomTypeDesc", "Package");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.PackageHotel)
                    .WithMany(p => p.HotelRoomTypeDesc)
                    .HasForeignKey(d => d.HotelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HotelRoomTypeDesc_Hotel");
            });

            modelBuilder.Entity<PackageCurrencyModel>(entity =>
            {
                entity.ToTable("Currency", "package");
                entity.HasKey(k => k.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.PackageCountry)
                    .WithMany(p => p.PackageCurrency)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Currency_Country");
            });

            modelBuilder.Entity<TourPackageModel>(entity =>
            {
                entity.ToTable("TourPackage", "Package");

                entity.HasIndex(e => e.PackageName)
                    .HasName("IX_TourPackage_Column");

                ////entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PackageName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Prefix)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Quote).HasMaxLength(200);

                entity.Property(e => e.Suffix)
                    .IsRequired()
                    .HasColumnType("nchar(10)");

                entity.Property(e => e.TourPackageType).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.IsHotelOnly);
            });

            modelBuilder.Entity<TourPackageCityModel>(entity =>
            {
                entity.ToTable("TourPackageCity", "Package");

                ////entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.TourPackageCityCity)
                    .WithMany(p => p.TourPackageCity)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourPackageCity_CityId");

                entity.HasOne(d => d.TourPackageCityState)
                    .WithMany(p => p.TourPackageCity)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourPackageCity_StateId");

                entity.HasOne(d => d.TourPackageCityCountry)
                    .WithMany(p => p.TourPackageCity)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourPackageCity_CountryId");

                entity.HasOne(d => d.TourPackageCityRegion)
               .WithMany(p => p.TourPackageCity)
               .HasForeignKey(d => d.RegionId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_TourPackageCity_RegionId");

                entity.HasOne(d => d.TourPackage)
                    .WithMany(p => p.TourPackageCity)
                    .HasForeignKey(d => d.TourPackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourPackageCity_TourPackage");
            });

            modelBuilder.Entity<TourPackageBookDateModel>(entity =>
            {
                entity.ToTable("TourPackageBookDate", "Package");

                ////entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.TourPackage)
                    .WithMany(p => p.TourPackageBookDate)
                    .HasForeignKey(d => d.TourPackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourPackageBookDate_TourPackage");
            });

            modelBuilder.Entity<TourPackageTravelStyleModel>(entity =>
            {
                entity.ToTable("TourPackageTravelStyle", "Package");

                ////entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.TourPackage)
                    .WithMany(p => p.TourPackageTravelStyle)
                    .HasForeignKey(d => d.TourPackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourPackageTravelStyle_TourPackage");
            });

            modelBuilder.Entity<TourPackageNightsDepartCityModel>(entity =>
            {
                entity.ToTable("TourPackageNightsDepartCity", "Package");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.TourPackageNight)
                    .WithMany(p => p.TourPackageNightsDepartCity)
                    .HasForeignKey(d => d.TourPackageNightsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourPackageNightsDepartCity_TourPackageNights");
            });

            modelBuilder.Entity<TourPackageNightsValidityModel>(entity =>
            {
                entity.ToTable("TourPackageNightsValidity", "Package");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.TourPackageNight)
                    .WithMany(p => p.TourPackageNightsValidity)
                    .HasForeignKey(d => d.TourPackageNightsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourPackageNightsValidity_TourPackageNights");
            });

            modelBuilder.Entity<TourPackageImageModel>(entity =>
            {
                entity.ToTable("TourPackageImage", "Package");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ImageDescription).HasMaxLength(100);

                entity.Property(e => e.ImageName).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getutcdate())");
            });
            modelBuilder.Entity<VendorCategoryModel>(entity =>
            {
                entity.ToTable("Category", "Vendor");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<DealsDepartureDatesModel>(entity =>
            {
                entity.ToTable("DepatureDates", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DealsNightsModel)
                    .WithMany(p => p.DealsDepartureDatesModels)
                    .HasForeignKey(d => d.NightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DepatureDates_DealNights");
            });
            modelBuilder.Entity<PackageMarginTypeModel>(entity =>
            {
                entity.ToTable("Margin", "Package");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            ////modelBuilder.Entity<PackagePromotionsModel>(entity =>
            ////{
            ////    entity.ToTable("Promotions", "Package");
            ////    entity.HasKey(k => k.Id);
            ////    entity.Property(e => e.Id).ValueGeneratedOnAdd();
            ////    entity.HasOne(d => d.MarginTypeModel)
            ////        .WithMany(d => d.PromotionsModels)
            ////        .HasForeignKey(k => k.Type)
            ////        .OnDelete(DeleteBehavior.ClientSetNull)
            ////        .HasConstraintName("FK_Promotion_Margin");
            ////});
            modelBuilder.Entity<Vendor_PackageModel>(entity =>
            {
                entity.ToTable("Vendor_Package", "dbo");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<PackageCancellation_PackageModel>(entity =>
            {
                entity.ToTable("Cancellation_Package", "Package");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<PackagePromotions_PackageModel>(entity =>
            {
                entity.ToTable("Promotions_Package", "Package");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<AmenitiesMasterModel>(entity =>
            {
                entity.ToTable("Amenity", "dbo");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<DestinationModel>(entity =>
            {
                entity.ToTable("Destination", "Package");
                entity.HasKey(k => k.D_Id);
                entity.Property(e => e.D_Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.PackageModel)
                    .WithMany(d => d.DestinationModels)
                    .HasForeignKey(k => k.D_PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Destination_Package");
                entity.HasOne(d => d.RegionModel)
                    .WithMany(d => d.DestinationModels)
                    .HasForeignKey(k => k.D_Region)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Destination_Region");
                entity.HasOne(d => d.CountryModel)
                    .WithMany(d => d.DestinationModels)
                    .HasForeignKey(k => k.D_Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Destination_Country");
                entity.HasOne(d => d.StateModel)
                    .WithMany(d => d.DestinationModels)
                    .HasForeignKey(k => k.D_State)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Destination_State");
                entity.HasOne(d => d.CityModel)
                    .WithMany(d => d.DestinationModels)
                    .HasForeignKey(k => k.D_City)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Destination_City");
            });
            modelBuilder.Entity<DestinationValidityModel>(entity =>
            {
                entity.ToTable("DestinationValidity", "Package");
                entity.HasKey(k => k.DV_Id);
                entity.Property(e => e.DV_Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DestinationModel)
                    .WithMany(d => d.DestinationValidityModels)
                    .HasForeignKey(k => k.DV_DestinationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DestinationValidity_Destination");
            });
            modelBuilder.Entity<BlockBookingModel>(entity =>
            {
                entity.ToTable("BlockBooking", "Package");
                entity.HasKey(k => k.BB_Id);
                entity.Property(e => e.BB_Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<ServiceTypeMasterModel>(entity =>
            {
                entity.ToTable("Service", "dbo");
                entity.HasKey(k => k.Id);
            });

            modelBuilder.Entity<VisaCountryMasterModel>(entity =>
            {
                entity.ToTable("VisaCountryMaster", "dbo");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TourPackageVisaInfoModel>(entity =>
            {
                entity.ToTable("TourPackageVisaInfo", "Package");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<InsuranceModel>(entity =>
            {
                entity.ToTable("Insurance", "dbo");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.VendorModel)
                       .WithMany(d => d.InsuranceModels)
                       .HasForeignKey(k => k.VendorID)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_Insurance_vendorID");
            });
            modelBuilder.Entity<TourPackageInsuranceInfoModel>(entity =>
            {
                entity.ToTable("TourPackageInsuranceInfo", "Package");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<OptionalToursItemsMasterModel>(entity =>
            {
                entity.ToTable("OptionalToursMasterItems", "dbo");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<TourPackageOptionalTourInfoModel>(entity =>
            {
                entity.ToTable("TourPackageOptionalTourInfo", "Package");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<CurationsModel>(entity =>
            {
                entity.ToTable("Curations", "dbo");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<BlogPostsModel>(entity =>
            {
                entity.ToTable("BlogPosts", "dbo");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VisaModel>(entity =>
            {
                entity.ToTable("Visa", "dbo");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.CountryModel)
                        .WithMany(d => d.VisaModels)
                        .HasForeignKey(k => k.CountryId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Visa_countryID");
                entity.HasOne(d => d.VendorModel)
                       .WithMany(d => d.VisaModels)
                       .HasForeignKey(k => k.VendorID)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_Visa_vendorID");
            });

            modelBuilder.Entity<VendorInformationModel>(entity =>
            {
                entity.ToTable("Information", "Vendor");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.CategoryModel)
                        .WithMany(d => d.VendorInfoModels)
                        .HasForeignKey(k => k.Category)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Information_Category");
                entity.HasOne(d => d.AreaModel)
                       .WithMany(d => d.VendorInformationModels)
                       .HasForeignKey(k => k.Area)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_Information_Area");
                entity.HasOne(d => d.CityModel)
                        .WithMany(d => d.VendorModels)
                        .HasForeignKey(k => k.City)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Information_City");
                ////entity.HasOne(d => d.StateModel)
                ////    .WithMany(d => d.VendorModels)
                ////    .HasForeignKey(k => k.State)
                ////    .OnDelete(DeleteBehavior.ClientSetNull)
                ////    .HasConstraintName("FK_Information_State");
                entity.HasOne(d => d.CountryModel)
                    .WithMany(d => d.VendorModels)
                    .HasForeignKey(k => k.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Information_Country");
                entity.HasOne(d => d.CurrencyModel)
                    .WithMany(d => d.VendorModels)
                    .HasForeignKey(k => k.Currency)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Information_Currency");
                entity.HasOne(d => d.VendorGroupModel)
                    .WithMany(d => d.VendorInformationModels)
                    .HasForeignKey(k => k.Group)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Information_Group");
            });
            modelBuilder.Entity<VendorServiceModel>(entity =>
            {
                entity.ToTable("Service", "Vendor");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.VendorModel)
                        .WithMany(d => d.VendorServiceModels)
                        .HasForeignKey(k => k.VendorId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Service_Information");
                entity.HasOne(d => d.ServiceTypeModel)
                        .WithMany(d => d.VendorServiceModels)
                        .HasForeignKey(k => k.ServiceId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Service_VendorService");
            });
            modelBuilder.Entity<VendorContactModel>(entity =>
            {
                entity.ToTable("Contact", "Vendor");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.VendorInformationModel)
                        .WithMany(d => d.VendorContactModels)
                        .HasForeignKey(k => k.VendorId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Contact_Information");
            });
            modelBuilder.Entity<VendorContractModel>(entity =>
            {
                entity.ToTable("Contract", "Vendor");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.VendorInformationModel)
                        .WithMany(d => d.VendorContractModels)
                        .HasForeignKey(k => k.VendorId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Contract_Information");
                entity.HasOne(d => d.MarginTypeModel)
                        .WithMany(d => d.VendorContractModels)
                        .HasForeignKey(k => k.MarginType)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Contract_Margin");
            });
            modelBuilder.Entity<VendorBankDetailModel>(entity =>
            {
                entity.ToTable("BankDetail", "Vendor");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.VendorInformationModel)
                        .WithMany(d => d.VendorBankDetailModels)
                        .HasForeignKey(k => k.VendorId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BankDetail_Information");
            });
            modelBuilder.Entity<VendorGroupModel>(entity =>
            {
                entity.ToTable("Group", "Vendor");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<CurrencyModel>(entity =>
            {
                entity.ToTable("Currency", "dbo");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.PackageCountry)
                        .WithMany(d => d.CurrencyModel)
                        .HasForeignKey(k => k.Country)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Currency_Country");
            });
            modelBuilder.Entity<DealsTypeModel>(entity =>
            {
                entity.ToTable("Type", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<DealsPackageModel>(entity =>
            {
                entity.ToTable("Package", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DealTypeModel)
                        .WithMany(d => d.DealsPackageModels)
                        .HasForeignKey(k => k.Type)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Package_Type");
            });

            modelBuilder.Entity<DealsNightModel>(entity =>
            {
                entity.ToTable("Night", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DealsPackageModel)
                        .WithMany(d => d.DealsNightModels)
                        .HasForeignKey(k => k.PackageId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Night_Package");
            });

            modelBuilder.Entity<DealsPaxCombinationModel>(entity =>
            {
                entity.ToTable("PaxCombination", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DealsPackageModel)
                        .WithMany(d => d.DealsPaxCombinationModels)
                        .HasForeignKey(k => k.PackageId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PaxCombination_Package");
            });
            modelBuilder.Entity<DealsContentModel>(entity =>
            {
                entity.ToTable("Content", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DealsPackageModel)
                        .WithMany(d => d.DealContentModels)
                        .HasForeignKey(k => k.PackageId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Content_Package");
            });
            modelBuilder.Entity<DealsInclusionModel>(entity =>
            {
                entity.ToTable("Inclusion", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DealsInclusionTypeModel)
                        .WithMany(d => d.DealsInclusionModels)
                        .HasForeignKey(k => k.TypeId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Inclusion_InclusionType");
                entity.HasOne(d => d.DealsItineraryModel)
                        .WithMany(d => d.DealsInclusionModels)
                        .HasForeignKey(k => k.ItineraryId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Inclusion_Itinerary");
            });
            modelBuilder.Entity<DealsAddOnModel>(entity =>
            {
                entity.ToTable("AddOn", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DealInclusionModel)
                        .WithMany(d => d.DealsAddOnModels)
                        .HasForeignKey(k => k.InclusionId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AddOn_Inclusion");
            });
            modelBuilder.Entity<DealsFlightModel>(entity =>
            {
                entity.ToTable("Flight", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DealsInclusionModel)
                       .WithMany(d => d.DealsFlightModels)
                       .HasForeignKey(k => k.InclusionId)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_Flight_Inclusion");
            });

            modelBuilder.Entity<DealsReviewModel>(entity =>
            {
                entity.ToTable("Review", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DealsPackageModel)
                        .WithMany(d => d.DealsReviewModels)
                        .HasForeignKey(k => k.PackageId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Review_Package");
            });
            modelBuilder.Entity<DealsImageModel>(entity =>
            {
                entity.ToTable("Image", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DealsPackageModel)
                        .WithMany(d => d.DealsImageModels)
                        .HasForeignKey(k => k.PackageId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Image_Package");
            });
            modelBuilder.Entity<DealsCancellationPolicyModel>(entity =>
            {
                entity.ToTable("CancellationPolicy", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DealsPackageModel)
                        .WithMany(d => d.DealsCancellationPolicyModels)
                        .HasForeignKey(k => k.PackageId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CancellationPolicy_Package");
                entity.HasOne(d => d.MarginTypeModel)
                        .WithMany(d => d.DealsCancellationPolicyModels)
                        .HasForeignKey(k => k.PackageId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CancellationPolicy_PackageMargin");
            });
            modelBuilder.Entity<DealsDestinationModel>(entity =>
            {
                entity.ToTable("Destination", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DealsPackageModel)
                        .WithMany(d => d.DealsDestinationModels)
                        .HasForeignKey(k => k.PackageId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Destination_Package");
            });

            modelBuilder.Entity<DealsBookingValidityModel>(entity =>
            {
                entity.ToTable("BookingValidity", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DealsPackageModel)
                        .WithMany(d => d.DealsBookingValidityModels)
                        .HasForeignKey(k => k.PackageId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BookingValidity_Package");
            });

            modelBuilder.Entity<DealsItineraryModel>(entity =>
            {
                entity.ToTable("Itinerary", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DealsNightModel)
                        .WithMany(d => d.DealsItineraryModels)
                        .HasForeignKey(k => k.NightId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Itinerary_Night");
            });

            modelBuilder.Entity<DealsRatePlanModel>(entity =>
            {
                entity.ToTable("RatePlan", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DealsNightModel)
                        .WithMany(d => d.DealsRatePlanModel)
                        .HasForeignKey(k => k.NightId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RatePlan_Night");
            });

            modelBuilder.Entity<DealsPromotion_RoomType>(entity =>
            {
                entity.ToTable("Promotion_RoomType", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.PackageHotelRoomModel)
                        .WithMany(d => d.DealPromotionRoomTypeModel)
                        .HasForeignKey(k => k.RoomTypeId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Promotion_RoomType_HotelRoomType");
                entity.HasOne(d => d.PromotionModel)
                        .WithMany(d => d.DealPromotionRoomTypeModel)
                        .HasForeignKey(k => k.PromotionId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Promotion_RoomType_Promotion");
            });

            modelBuilder.Entity<SalutationModel>(entity =>
            {
                entity.ToTable("Salutation", "dbo");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<DesignationModel>(entity =>
            {
                entity.ToTable("Designation", "dbo");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<HotelierPropertyTypeModel>(entity =>
            {
                entity.ToTable("PropertyType", "Hotelier");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<HotelierRoomConfigurationModel>(entity =>
            {
                entity.ToTable("RoomConfiguration", "Hotelier");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.HotelierInformationModel)
                        .WithMany(d => d.HotelierRoomConfigModels)
                        .HasForeignKey(k => k.HotelId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RoomConfiguration_Information");
                entity.HasOne(d => d.PackageHotelRoomTypeModel)
                       .WithMany(d => d.HotelierRoomConfigurationModels)
                       .HasForeignKey(k => k.RoomTypeId)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_RoomConfiguration_HotelRoomType");
            });
            modelBuilder.Entity<HotelierInformationModel>(entity =>
            {
                entity.ToTable("Information", "Hotelier");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.HotelierPropertyTypeModel)
                        .WithMany(d => d.HotelierInformationModels)
                        .HasForeignKey(k => k.PropertyType)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Information_PropertyType");
                entity.HasOne(d => d.VendorInformationModel)
                       .WithMany(d => d.HotelierInformationModels)
                       .HasForeignKey(k => k.VendorId)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_Hotelier_Vendor");
            });
            modelBuilder.Entity<HotelierContentModel>(entity =>
            {
                entity.ToTable("Content", "Hotelier");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.HotelierInformationModel)
                        .WithMany(d => d.HotelierContentsModels)
                        .HasForeignKey(k => k.HotelId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Content_Information");
            });
            modelBuilder.Entity<HotelierAmenitiesModel>(entity =>
            {
                entity.ToTable("Amenities", "Hotelier");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.HotelierInformationModel)
                      .WithMany(d => d.HotelierAmenitiesModels)
                      .HasForeignKey(k => k.HotelId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Amenities_Information");
                entity.HasOne(d => d.AmenitiesMasterModel)
                       .WithMany(d => d.HotelierAmenitiesModels)
                       .HasForeignKey(k => k.AmentieId)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_Ameneties_Amenity");
            });
            modelBuilder.Entity<HotelierRoomAmenetiesModel>(entity =>
            {
                entity.ToTable("RoomAmenities", "Hotelier");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.HotelierRoomConfigModel)
                      .WithMany(d => d.HotelierRoomAmenetiesModels)
                      .HasForeignKey(k => k.RoomConfigId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_RoomAmenity_RoomConfiguration");
                entity.HasOne(d => d.AmenitiesMasterModel)
                       .WithMany(d => d.HotelierRoomAmenetiesModels)
                       .HasForeignKey(k => k.AmenetieId)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_RoomAmenity_Amenity");
            });
            modelBuilder.Entity<HotelierRoomImageModel>(entity =>
            {
                entity.ToTable("RoomImages", "Hotelier");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.HotelierRoomConfigurationModel)
                      .WithMany(d => d.HotelierRoomImageModels)
                      .HasForeignKey(k => k.RoomConfigId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_RoomImages_RoomConfiguration");
            });
            modelBuilder.Entity<HotelierImageModel>(entity =>
            {
                entity.ToTable("Image", "Hotelier");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.HotelierInformationModel)
                      .WithMany(d => d.HotelierImageModels)
                      .HasForeignKey(k => k.HotelId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Image_Information");
            });
            modelBuilder.Entity<HotelierReviewModel>(entity =>
            {
                entity.ToTable("Review", "Hotelier");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.HotelierInformationModel)
                      .WithMany(d => d.HotelierReviewModels)
                      .HasForeignKey(k => k.HotelId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_CustomerReview_Hotel");
            });
            modelBuilder.Entity<CancellationPolicyModel>(entity =>
            {
                entity.ToTable("CancellationPolicy", "dbo");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.MarginTypeModel)
                      .WithMany(d => d.CancellationPolicyModels)
                      .HasForeignKey(k => k.ChargeType)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_CancellationPolicy_Margin");
            });
            modelBuilder.Entity<HotelierCancellationPolicyModel>(entity =>
            {
                entity.ToTable("CancellationPolicy", "Hotelier");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.MarginTypeModel)
                      .WithMany(d => d.HotelierCancellationPolicyModels)
                      .HasForeignKey(k => k.MarginType)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_CancellationPolicy_Margin");
            });
            modelBuilder.Entity<DealVisaModel>(entity =>
            {
                entity.ToTable("Visa", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DealsPackageModel)
                      .WithMany(d => d.DealsVisaModels)
                      .HasForeignKey(k => k.PackageId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Visa_Package");
            });
            modelBuilder.Entity<DealsHighlightModel>(entity =>
            {
                entity.ToTable("Highlight", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DealsPackageModel)
                      .WithMany(d => d.DealsHighlightModels)
                      .HasForeignKey(k => k.PackageId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Highlight_Package");
            });
            modelBuilder.Entity<DealRoomConfigurationModel>(entity =>
            {
                entity.ToTable("RoomConfiguration", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.PackageHotelRoomTypeModel)
                      .WithMany(d => d.DealsRoomConfigurationModels)
                      .HasForeignKey(k => k.RoomTypeId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_RoomConfiguration_HotelRoomType");
                entity.HasOne(d => d.DealInclusionModel)
                      .WithMany(d => d.DealRoomConfigurationModels)
                      .HasForeignKey(k => k.InclusionId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_RoomConfiguration_Inclusion");
            });
            modelBuilder.Entity<HotelierPromotionModel>(entity =>
            {
                entity.ToTable("Promotion", "Hotelier");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.PromotionTypeModel)
                      .WithMany(d => d.HotelierPromotionModels)
                      .HasForeignKey(k => k.Type)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Promotion_PromotionType");
                entity.HasOne(d => d.MarginTypeModel)
                      .WithMany(d => d.HotelierPromotionModels)
                      .HasForeignKey(k => k.DiscountType)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Promotion_Margin");
                entity.HasOne(d => d.HotelierInformationModel)
                      .WithMany(d => d.HotelierPromotionModels)
                      .HasForeignKey(k => k.HotelId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Promotion_Information");
            });
            modelBuilder.Entity<DealsPromotionModel>(entity =>
            {
                entity.ToTable("Promotion", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DealPackageModel)
                      .WithMany(d => d.DealsPromotionModels)
                      .HasForeignKey(k => k.PackageId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Promotion_Package");
                entity.HasOne(d => d.MarginTypeModel)
                       .WithMany(d => d.DealsPromotionModels)
                       .HasForeignKey(k => k.DiscountType)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_Promotion_Margin");
                entity.HasOne(d => d.PromotionTypeModel)
                       .WithMany(d => d.DealsPromotionModels)
                       .HasForeignKey(k => k.Type)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_Promotion_PromotionType");
            });
            modelBuilder.Entity<DealsPromoScheduleModel>(entity =>
            {
                entity.ToTable("PromoSchedule", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DealPackageModel)
                       .WithMany(d => d.DealsPromoScheduleModel)
                       .HasForeignKey(k => k.PackageId)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_PromoSchedule_Package");
                entity.HasOne(d => d.SettingPromoTypeModel)
                       .WithMany(d => d.DealPromoScheduleModel)
                       .HasForeignKey(k => k.PromoType)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_PromoSchedule_PromoType");
            });
            modelBuilder.Entity<SettingPromoType>(entity =>
            {
                entity.ToTable("PromoType", "Setting");
                entity.HasKey(k => k.Id);
            });
            modelBuilder.Entity<SettingPromoDiscountModel>(entity =>
            {
                entity.ToTable("PromoDiscount", "Setting");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<BookingInformationModel>(entity =>
            {
                entity.ToTable("Information", "Booking");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DealsPackageModel)
                       .WithMany(d => d.BookingInformationModels)
                       .HasForeignKey(k => k.DealId)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_Information_Package");
            });
            modelBuilder.Entity<BookingHotelRoomModel>(entity =>
            {
                entity.ToTable("HotelRoom", "Booking");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.BookingInformationModel)
                       .WithMany(d => d.BookingHotelRoomModels)
                       .HasForeignKey(k => k.BookingId)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_HotelRoom_Information");
            });
            modelBuilder.Entity<BookingFlightModel>(entity =>
            {
                entity.ToTable("Flight", "Booking");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.BookingInformationModel)
                       .WithMany(d => d.BookingFlightModels)
                       .HasForeignKey(k => k.BookingId)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_Flight_Information");
            });
            modelBuilder.Entity<BookingFlightModel>(entity =>
            {
                entity.ToTable("Flight", "Booking");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.BookingInformationModel)
                       .WithMany(d => d.BookingFlightModels)
                       .HasForeignKey(k => k.BookingId)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_Flight_Information");
            });
            modelBuilder.Entity<BookingPassengerModel>(entity =>
            {
                entity.ToTable("Passenger", "Booking");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.BookingInformationModel)
                       .WithMany(d => d.BookingPassengerModels)
                       .HasForeignKey(k => k.BookingId)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_Passenger_Information");
            });
            modelBuilder.Entity<BookingRoomPriceBreakupModel>(entity =>
            {
                entity.ToTable("RoomPriceBreakup", "Booking");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.BookingHotelRoomModel)
                       .WithMany(d => d.BookingRoomPriceBreakupModels)
                       .HasForeignKey(k => k.HotelRoomId)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_RoomPriceBreakup_HotelRoom");
            });
            modelBuilder.Entity<BookingVisaModel>(entity =>
            {
                entity.ToTable("Visa", "Booking");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.BookingInformationModel)
                       .WithMany(d => d.BookingVisaModels)
                       .HasForeignKey(k => k.BookingId)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_Visa_Information");
            });
            modelBuilder.Entity<DealInventoryModel>(entity =>
            {
                entity.ToTable("Inventory", "Deals");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DealRatePlanModel)
                       .WithMany(d => d.DealInventoryModels)
                       .HasForeignKey(k => k.RatePlanId)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_Itinerary_Night");
            });
            modelBuilder.Entity<LoginModel>(entity =>
            {
                entity.ToTable("Login", "TBO");
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Ignores the columns.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        private static void IgnoreColumns(ModelBuilder modelBuilder)
        {
        }
    }
}