using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PointOfSale.DAL.Domains;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using IdentityServer4.EntityFramework.Options;

namespace PointOfSale.BLL.Contexts
{
    public class POSContext : ApiAuthorizationDbContext<SahlUserIdentity>
    {
        public POSContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
   
       // public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AddressType> AddressTypes { get; set; }
        public virtual DbSet<BarcodeScanner> BarcodeScanners { get; set; }
        public virtual DbSet<BarcodeType> BarcodeTypes { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CountryGroup> CountryGroups { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<CustomerContact> CustomerContacts { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<FiscalPointOfSaleition> FiscalPointOfSaleitions { get; set; }
        public virtual DbSet<Floor> Floors { get; set; }
        public virtual DbSet<FloorTable> FloorTables { get; set; }
        public virtual DbSet<LoyaltyProgram> LoyaltyPrograms { get; set; }
        public virtual DbSet<OptionalProduct> OptionalProducts { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<Pricelist> Pricelists { get; set; }
        public virtual DbSet<PriceRule> PriceRules { get; set; }
        public virtual DbSet<ShopPrinter> Printers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Reward> Rewards { get; set; }
        public virtual DbSet<Rules> Rules { get; set; }
        public virtual DbSet<SahlApplication> SahlApplications { get; set; }
        public virtual DbSet<SahlApplicationsCompanies> SahlApplicationsCompanies { get; set; }
        public virtual DbSet<SahlUserIdentity> SahlUserIdentity { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Tax> Taxes { get; set; }
        public virtual DbSet<TaxMapping> TaxMappings { get; set; }
        public virtual DbSet<Shop> Shop { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<VendorProduct> VendorProducts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerTax> CustomerTaxs { get; set; }
        public virtual DbSet<VendorTax> VendorTaxes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<ApplicationUserRole>(userRole =>
            //{
            //    userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

            //    userRole.HasOne(ur => ur.Role)
            //        .WithMany(r => r.UserRoles)
            //        .HasForeignKey(ur => ur.RoleId)
            //        .IsRequired();

            //    userRole.HasOne(ur => ur.User)
            //        .WithMany(r => r.UserRoles)
            //        .HasForeignKey(ur => ur.UserId)
            //        .IsRequired();
            //});
        }
    }
}
