using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Core.Entities;

namespace RealEstate.Infrastructure
{
    public class RealEstateDbContext : DbContext
    {
        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Builder> Builders { get; set; }
        public DbSet<AgentExpertise> AgentExpertise { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<BuilderSpecialty> BuilderSpecialties { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<UserCertification> UserCertifications { get; set; }
        public DbSet<AgentReview> AgentReviews { get; set; }
        public DbSet<BuilderReview> BuilderReviews { get; set; }
        public DbSet<AgentStats> AgentStats { get; set; }
        public DbSet<BuilderStats> BuilderStats { get; set; }
        public DbSet<BuilderProject> BuilderProjects { get; set; }
        public DbSet<ProjectMedia> ProjectMedia { get; set; }
        public DbSet<BuilderAward> BuilderAwards { get; set; }
        public DbSet<BuilderContactInquiry> BuilderContactInquiries { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyMedia> PropertyMedia { get; set; }
        public DbSet<PropertyFeatures> PropertyFeatures { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<PropertyAmenities> PropertyAmenities { get; set; }
        public DbSet<PropertyNearby> PropertyNearby { get; set; }
        public DbSet<PropertyInquiry> PropertyInquiries { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================
            // 1. User
            // ==========================
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.UserID);

                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Role);

                entity.Property(e => e.Role).HasMaxLength(20);
                entity.Property(e => e.FullName).HasMaxLength(150).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(150).IsRequired();
                entity.Property(e => e.PhoneNumber).HasMaxLength(30);
                entity.Property(e => e.ProfilePictureURL).HasMaxLength(255);

                entity.Property(e => e.IsVerified).HasDefaultValue(false);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasCheckConstraint("CHK_Users_Role", "Role IN ('Admin','Agent','Builder')");
            });

            // ==========================
            // 2. Agent (1:1 with User)
            // ==========================
            modelBuilder.Entity<Agent>(entity =>
            {
                entity.ToTable("Agents");
                entity.HasKey(e => e.AgentID);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Agent)
                    .HasForeignKey<Agent>(d => d.UserID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.UserID).IsUnique();
                entity.HasIndex(e => e.Location);

                entity.Property(e => e.IsVerified).HasDefaultValue(false);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
            });

            // ==========================
            // 3. Builder (1:1 with User)
            // ==========================
            modelBuilder.Entity<Builder>(entity =>
            {
                entity.ToTable("Builders");
                entity.HasKey(e => e.BuilderID);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Builder)
                    .HasForeignKey<Builder>(d => d.UserID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.UserID).IsUnique();
                entity.HasIndex(e => e.Location);
                entity.HasIndex(e => e.Slug).IsUnique();

                entity.Property(e => e.CompanyName).HasMaxLength(150);
                entity.Property(e => e.Slug).HasMaxLength(150);
                entity.Property(e => e.ContactEmail).HasMaxLength(150);
                entity.Property(e => e.ContactPhone).HasMaxLength(20);
                entity.Property(e => e.WebsiteURL).HasMaxLength(200);
            });

            // ==========================
            // 4. AgentExpertise
            // ==========================
            modelBuilder.Entity<AgentExpertise>(entity =>
            {
                entity.ToTable("AgentExpertise");
                entity.HasKey(e => e.ExpertiseID);

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentExpertise)
                    .HasForeignKey(d => d.AgentID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.HasIndex(e => e.AgentID);
            });

            // ==========================
            // 5. Specialty & BuilderSpecialty (Many-to-Many)
            // ==========================
            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.ToTable("Specialties");
                entity.HasKey(e => e.SpecialtyID);
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<BuilderSpecialty>(entity =>
            {
                entity.ToTable("BuilderSpecialties");
                entity.HasKey(e => e.BuilderSpecialtyID);

                entity.HasOne(d => d.Builder)
                    .WithMany(p => p.BuilderSpecialties)
                    .HasForeignKey(d => d.BuilderID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.BuilderSpecialties)
                    .HasForeignKey(d => d.SpecialtyID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => new { e.BuilderID, e.SpecialtyID }).IsUnique();
                entity.HasIndex(e => e.BuilderID);
            });

            // ==========================
            // 6. Certification & UserCertification
            // ==========================
            modelBuilder.Entity<Certification>(entity =>
            {
                entity.ToTable("Certifications");
                entity.HasKey(e => e.CertificationID);
                entity.Property(e => e.Name).HasMaxLength(150).IsRequired();
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<UserCertification>(entity =>
            {
                entity.ToTable("UserCertifications");
                entity.HasKey(e => e.UserCertificationID);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCertifications)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Certification)
                    .WithMany(p => p.UserCertifications)
                    .HasForeignKey(d => d.CertificationID);

                entity.HasIndex(e => e.UserID);
            });

            // ==========================
            // Reviews, Stats, Projects, etc. (abbreviated for clarity — all included below)
            // ==========================

            ConfigureAgentReview(modelBuilder);
            ConfigureBuilderReview(modelBuilder);
            ConfigureAgentStats(modelBuilder);
            ConfigureBuilderStats(modelBuilder);
            ConfigureBuilderProject(modelBuilder);
            ConfigureProjectMedia(modelBuilder);
            ConfigureBuilderAward(modelBuilder);
            ConfigureBuilderContactInquiry(modelBuilder);
            ConfigureProperty(modelBuilder);
            ConfigurePropertyMedia(modelBuilder);
            ConfigurePropertyFeatures(modelBuilder);
            ConfigureAmenity(modelBuilder);
            ConfigurePropertyAmenities(modelBuilder);
            ConfigurePropertyNearby(modelBuilder);
            ConfigurePropertyInquiry(modelBuilder);
            ConfigurePayment(modelBuilder);
        }

        // Separate methods for clean organization
        private static void ConfigureAgentReview(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgentReview>(entity =>
            {
                entity.ToTable("AgentReviews");
                entity.HasKey(e => e.ReviewID);

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentReviews)
                    .HasForeignKey(d => d.AgentID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Reviewer)
                    .WithMany()
                    .HasForeignKey(d => d.ReviewerID)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.Property(e => e.Rating).HasColumnType("TINYINT");
                entity.HasCheckConstraint("CHK_AgentReviews_Rating", "Rating >= 1 AND Rating <= 5");
                entity.HasIndex(e => e.AgentID);
            });
        }

        // ... (all other configurations similarly — full code continues below)

        // I'll now paste the **complete remaining configurations**:

        private static void ConfigureBuilderReview(ModelBuilder modelBuilder) => modelBuilder.Entity<BuilderReview>(entity =>
        {
            entity.ToTable("BuilderReviews");
            entity.HasKey(e => e.ReviewID);

            entity.HasOne(d => d.Builder)
                .WithMany(p => p.BuilderReviews)
                .HasForeignKey(d => d.BuilderID)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Reviewer)
                .WithMany()
                .HasForeignKey(d => d.ReviewerID)
                .OnDelete(DeleteBehavior.SetNull);

            entity.Property(e => e.Rating).HasColumnType("TINYINT");
            entity.HasCheckConstraint("CHK_BuilderReviews_Rating", "Rating >= 1 AND Rating <= 5");
        });

        private static void ConfigureAgentStats(ModelBuilder modelBuilder) => modelBuilder.Entity<AgentStats>(entity =>
        {
            entity.ToTable("AgentStats");
            entity.HasKey(e => e.StatsID);
            entity.HasIndex(e => e.AgentID).IsUnique();

            entity.HasOne(d => d.Agent)
                .WithOne(p => p.AgentStats)
                .HasForeignKey<AgentStats>(d => d.AgentID)
                .OnDelete(DeleteBehavior.Cascade);
        });

        private static void ConfigureBuilderStats(ModelBuilder modelBuilder) => modelBuilder.Entity<BuilderStats>(entity =>
        {
            entity.ToTable("BuilderStats");
            entity.HasKey(e => e.StatsID);
            entity.HasIndex(e => e.BuilderID).IsUnique();

            entity.HasOne(d => d.Builder)
                .WithOne(p => p.BuilderStats)
                .HasForeignKey<BuilderStats>(d => d.BuilderID)
                .OnDelete(DeleteBehavior.Cascade);
        });

        private static void ConfigureBuilderProject(ModelBuilder modelBuilder) => modelBuilder.Entity<BuilderProject>(entity =>
        {
            entity.ToTable("BuilderProjects");
            entity.HasKey(e => e.ProjectID);

            entity.HasOne(d => d.Builder)
                .WithMany(p => p.BuilderProjects)
                .HasForeignKey(d => d.BuilderID)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasCheckConstraint("CHK_BuilderProjects_Status", "Status IN ('planned','ongoing','completed','paused')");
            entity.HasIndex(e => e.BuilderID);
        });

        private static void ConfigureProjectMedia(ModelBuilder modelBuilder) => modelBuilder.Entity<ProjectMedia>(entity =>
        {
            entity.ToTable("ProjectMedia");
            entity.HasKey(e => e.MediaID);

            entity.HasOne(d => d.Project)
                .WithMany(p => p.ProjectMedia)
                .HasForeignKey(d => d.ProjectID)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.MediaType).HasMaxLength(20);
            entity.HasCheckConstraint("CHK_ProjectMedia_MediaType", "MediaType IN ('image','video')");
            entity.HasIndex(e => new { e.ProjectID, e.DisplayOrder });
        });

        private static void ConfigureBuilderAward(ModelBuilder modelBuilder) => modelBuilder.Entity<BuilderAward>(entity =>
        {
            entity.ToTable("BuilderAwards");
            entity.HasKey(e => e.AwardID);

            entity.HasOne(d => d.Builder)
                .WithMany(p => p.BuilderAwards)
                .HasForeignKey(d => d.BuilderID)
                .OnDelete(DeleteBehavior.Cascade);
        });

        private static void ConfigureBuilderContactInquiry(ModelBuilder modelBuilder) => modelBuilder.Entity<BuilderContactInquiry>(entity =>
        {
            entity.ToTable("BuilderContactInquiries");
            entity.HasKey(e => e.InquiryID);

            entity.HasOne(d => d.Builder)
                .WithMany(p => p.BuilderContactInquiries)
                .HasForeignKey(d => d.BuilderID)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.Status).HasDefaultValue("new");
        });

        private static void ConfigureProperty(ModelBuilder modelBuilder) => modelBuilder.Entity<Property>(entity =>
        {
            entity.ToTable("Properties");
            entity.HasKey(e => e.PropertyID);

            entity.HasOne(d => d.Agent)
                .WithMany(p => p.Properties)
                .HasForeignKey(d => d.AgentID)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasCheckConstraint("CHK_Properties_PropertyType", "PropertyType IN ('Commercial','Land','Apartment','House')");
            entity.HasCheckConstraint("CHK_Properties_Status", "Status IN ('Approved','Pending','For Rent','For Sale')");
            entity.HasCheckConstraint("CHK_Properties_ListingType", "ListingType IN ('Rent','Sale')");

            entity.HasIndex(e => e.AgentID);
            entity.HasIndex(e => e.City);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => new { e.City, e.Status, e.Price });
        });

        private static void ConfigurePropertyMedia(ModelBuilder modelBuilder) => modelBuilder.Entity<PropertyMedia>(entity =>
        {
            entity.ToTable("PropertyMedia");
            entity.HasKey(e => e.MediaID);

            entity.HasOne(d => d.Property)
                .WithMany(p => p.PropertyMedia)
                .HasForeignKey(d => d.PropertyID)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.MediaType).HasMaxLength(10);
            entity.HasCheckConstraint("CHK_PropertyMedia_MediaType", "MediaType IN ('Image','Video')");
            entity.HasIndex(e => new { e.PropertyID, e.DisplayOrder });
        });

        private static void ConfigurePropertyFeatures(ModelBuilder modelBuilder) => modelBuilder.Entity<PropertyFeatures>(entity =>
        {
            entity.ToTable("PropertyFeatures");
            entity.HasKey(e => e.FeatureID);

            entity.HasOne(d => d.Property)
                .WithMany(p => p.PropertyFeatures)
                .HasForeignKey(d => d.PropertyID)
                .OnDelete(DeleteBehavior.Cascade);
        });

        private static void ConfigureAmenity(ModelBuilder modelBuilder) => modelBuilder.Entity<Amenity>(entity =>
        {
            entity.ToTable("Amenities");
            entity.HasKey(e => e.AmenityID);
            entity.Property(e => e.AmenityName).HasMaxLength(100).IsRequired();
            entity.HasIndex(e => e.AmenityName).IsUnique();
        });

        private static void ConfigurePropertyAmenities(ModelBuilder modelBuilder) => modelBuilder.Entity<PropertyAmenities>(entity =>
        {
            entity.ToTable("PropertyAmenities");
            entity.HasKey(e => e.PropertyAmenityID);

            entity.HasOne(d => d.Property)
                .WithMany(p => p.PropertyAmenities)
                .HasForeignKey(d => d.PropertyID)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Amenity)
                .WithMany(p => p.PropertyAmenities)
                .HasForeignKey(d => d.AmenityID);

            entity.HasIndex(e => new { e.PropertyID, e.AmenityID }).IsUnique();
        });

        private static void ConfigurePropertyNearby(ModelBuilder modelBuilder) => modelBuilder.Entity<PropertyNearby>(entity =>
        {
            entity.ToTable("PropertyNearby");
            entity.HasKey(e => e.NearbyID);

            entity.HasOne(d => d.Property)
                .WithMany(p => p.PropertyNearby)
                .HasForeignKey(d => d.PropertyID)
                .OnDelete(DeleteBehavior.Cascade);
        });

        private static void ConfigurePropertyInquiry(ModelBuilder modelBuilder) => modelBuilder.Entity<PropertyInquiry>(entity =>
        {
            entity.ToTable("PropertyInquiries");
            entity.HasKey(e => e.InquiryID);

            entity.HasOne(d => d.Property)
                .WithMany(p => p.PropertyInquiries)
                .HasForeignKey(d => d.PropertyID)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.Status).HasDefaultValue("New");
        });

        private static void ConfigurePayment(ModelBuilder modelBuilder) => modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payments");
            entity.HasKey(e => e.PaymentID);

            entity.HasOne(d => d.User)
                .WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Property)
                .WithMany(p => p.Payments)
                .HasForeignKey(d => d.PropertyID)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasCheckConstraint("CHK_Payments_Method", "PaymentMethod IN ('Card','BankTransfer','Stripe')");
            entity.HasCheckConstraint("CHK_Payments_Status", "PaymentStatus IN ('Pending','Success','Failed')");

            entity.HasIndex(e => e.UserID);
            entity.HasIndex(e => e.PropertyID);
            entity.HasIndex(e => e.TransactionID);
        });
    }

}
