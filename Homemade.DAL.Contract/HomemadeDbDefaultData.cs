using Homemade.Model;
using Homemade.Model.Driver;
using Homemade.Model.Emp;
using Homemade.Model.Setting;
using Microsoft.EntityFrameworkCore;
using System;

namespace Homemade.DAL.Contract
{
    public class HomemadeDbDefaultData
    {
        private static ModelBuilder _modelBuilder;
        private static readonly int _UserId = 1;
        /// <summary>
        /// This Method Is To Add Default Data To Db using in Migration
        /// </summary>
        /// <param name="modelBuilder"></param>
        internal static void Seed(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            AddUser();
            AddCustomRole();
            AddPermissionController();
            AddPermissionAction();
            AddPermissionControllerAction();
            AddCountry();
            AddRegion();
            AddCity();
            AddNationality();
            AddTransactionType();
            AddDeliverySetting();
            AddJobs();
            AddMainCategory();
            AddDepartments();
            AddEmployees();
            AddRoleConfig();
        }
        #region Main Data
        private static void AddUser()
        {
            _modelBuilder.Entity<User>().HasData(new User()
            {
                UserType = 1,
                UserName = "SystemUser",
                PhoneNumberConfirmed = true,
                PhoneNumber = "012",
                Email = "SystemUser@Admin.com",
                //Admin@123
                PasswordHash = "AQAAAAEAACcQAAAAEP65QXLX6e94ehLc9ntv07Q7n/aO6wf8y6j/z15XE7hfgyZLCNvHmM3Ar6SaTwzC3g==",
                Id = 1,
                NormalizedUserName = "SYSTEMUSER",
                NormalizedEmail = "SystemUser@Admin.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            });
        }
        private static void AddEmployees()
        {
            _modelBuilder.Entity<Employees>().HasData(new Employees()
            {
                EntityEmpID = 1,
                AddressAR = "",
                AddressEN = "",
                BirthDateHijri = "",
                Email = "SystemUser@Admin.com",
                //Admin@123
                BloodTypeId = 1,
                CityID = 1,
                CreateDate = DateTime.Now,
                EnableDate = DateTime.Now,
                EntityEmpGuid = Guid.Parse("2299447C-FC61-4AA4-BA03-8C91E4F4B2D5"),
                FileNo = "000000",
                FirstNameAR = "",
                FirstNameEN = "",
                IDNo = "",
                IsDeleted = false,
                Gender = 1,
                JobsID = 1,
                LastNameAR = "",
                LastNameEN = "",
                Lat = "",
                Lng = "",
                MidNameAR = "",
                MidNameEN = "",
                MobileNo = "00000000000",
                IsEnable = true,
                NationalityID = 1,
                Notes = "",
                PhoneNumber = "",
                Photo = "",
                UserEnable = 1,
                UserId = 1,
                Zoom = "",
            });
        }
        private static void AddCountry()
        {
            _modelBuilder.Entity<Country>().HasData(new Country()
            {
                CountryID = 1,
                CreateDate = DateTime.Now,
                IsDeleted = false,
                NameAR = "السعودية",
                NameEN = "SA",
                CountryGuid = Guid.NewGuid(),
                UserId = _UserId,
                IsEnable = true,
                EnableDate = DateTime.Now,
                Extension = "00966",
                Lat = "",
                Long = "",
                Place = "",
            });
        }
        private static void AddRegion()
        {
            _modelBuilder.Entity<Region>().HasData(new Region()
            {
                RegionID = 1,
                RegionGuid = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                IsDeleted = false,
                NameAR = "الدمام",
                NameEN = "DMM",
                UserId = _UserId,
                IsEnable = true,
                EnableDate = DateTime.Now,
                Lat = "",
                Long = "",
                Place = "",
                CountryID = 1,

            });
        }
        private static void AddCity()
        {
            _modelBuilder.Entity<City>().HasData(new City()
            {
                CityID = 1,
                RegionID = 1,
                CreateDate = DateTime.Now,
                IsDeleted = false,
                NameAR = "الدمام",
                NameEN = "DMM",
                CityGuid = Guid.NewGuid(),
                UserId = _UserId,
                IsEnable = true,
                EnableDate = DateTime.Now,
                Lat = "",
                Long = "",
                Place = "",
            });
        }
        private static void AddNationality()
        {
            _modelBuilder.Entity<Nationality>().HasData(new Nationality()
            {
                NationalityID = 1,
                CreateDate = DateTime.Now,
                IsDeleted = false,
                NameAR = "سعودي",
                NameEN = "Sauidian",
                NationalityGuid = Guid.NewGuid(),
                UserId = _UserId,
                IsEnable = true,
                EnableDate = DateTime.Now,
            });
        }
        private static void AddJobs()
        {
            _modelBuilder.Entity<Jobs>().HasData(new Jobs()
            {
                JobsID = 1,
                CreateDate = DateTime.Now,
                IsDeleted = false,
                NameAR = "مشرف",
                NameEN = "Visor",
                JobsGuid = Guid.NewGuid(),
                UserId = _UserId,
                IsEnable = true,
                EnableDate = DateTime.Now,
                JobTypeId = 2,

            });
        }
        private static void AddMainCategory()
        {
            _modelBuilder.Entity<MainCategory>().HasData(new MainCategory()
            {
                CreateDate = DateTime.Now,
                IsDeleted = false,
                NameAR = "تصنيف",
                NameEN = "Category",
                MainCategoryGuid = Guid.NewGuid(),
                UserId = _UserId,
                IsEnable = true,
                EnableDate = DateTime.Now,
                MainCategoryID = 1
            });
        }
        private static void AddDepartments()
        {
            _modelBuilder.Entity<Departments>().HasData(new Departments()
            {
                DepartmentsID = 1,
                CreateDate = DateTime.Now,
                IsDeleted = false,
                NameAR = "قسم جديد",
                NameEN = "New Department",
                DepartmentsGuid = Guid.NewGuid(),
                UserId = _UserId,
                IsEnable = true,
                EnableDate = DateTime.Now,
                MainCategoryID = 1
            });
        }
        private static void AddTransactionType()
        {
            _modelBuilder.Entity<TransactionType>().HasData(
                new TransactionType()
                {
                    CreateDate = new DateTime(2020, 9, 1, 12, 00, 00),
                    IsDeleted = false,
                    TransactionTypeGuid = Guid.NewGuid(),
                    NameAR = "شحن رصيد بواسطة الإدارة",
                    NameEN = "Deposit by Operation",
                    TransactionTypeID = 1,
                    UserId = _UserId,
                },
                  new TransactionType()
                  {
                      CreateDate = new DateTime(2020, 9, 1, 12, 00, 00),
                      IsDeleted = false,
                      TransactionTypeGuid = Guid.NewGuid(),
                      NameAR = "سحب - جزاء",
                      NameEN = "Withdrawal",
                      TransactionTypeID = 2,
                      UserId = _UserId,
                  },
                  new TransactionType()
                  {
                      CreateDate = new DateTime(2020, 9, 1, 12, 00, 00),
                      IsDeleted = false,
                      TransactionTypeGuid = Guid.NewGuid(),
                      NameAR = "علاوة",
                      NameEN = "Bouns",
                      TransactionTypeID = 3,
                      UserId = _UserId,
                  },
                  new TransactionType()
                  {
                      CreateDate = new DateTime(2020, 9, 1, 12, 00, 00),
                      IsDeleted = false,
                      TransactionTypeGuid = Guid.NewGuid(),
                      NameAR = "عقوبة",
                      NameEN = "Punishment",
                      TransactionTypeID = 4,
                      UserId = _UserId,
                  },
            new TransactionType()
            {
                CreateDate = new DateTime(2020, 9, 1, 12, 00, 00),
                IsDeleted = false,
                TransactionTypeGuid = Guid.NewGuid(),
                NameAR = "دفع للكابتن STC",
                NameEN = "Pay to Captain STC",
                TransactionTypeID = 5,
                UserId = _UserId,
            },
            new TransactionType()
            {
                CreateDate = new DateTime(2020, 9, 1, 12, 00, 00),
                IsDeleted = false,
                TransactionTypeGuid = Guid.NewGuid(),
                NameAR = "توصيل طلب",
                NameEN = "Delivery Order",
                TransactionTypeID = 6,
                UserId = _UserId,
            },
            new TransactionType()
            {
                CreateDate = new DateTime(2020, 9, 1, 12, 00, 00),
                IsDeleted = false,
                TransactionTypeGuid = Guid.NewGuid(),
                NameAR = "دفع طلب",
                NameEN = "Pay Order",
                TransactionTypeID = 7,
                UserId = _UserId,
            },
            new TransactionType()
            {
                CreateDate = new DateTime(2020, 9, 1, 12, 00, 00),
                IsDeleted = false,
                TransactionTypeGuid = Guid.NewGuid(),
                NameAR = "دفع فاتورة",
                NameEN = "Pay Invoice",
                TransactionTypeID = 8,
                UserId = _UserId,
            },
            new TransactionType()
            {
                CreateDate = new DateTime(2020, 9, 1, 12, 00, 00),
                IsDeleted = false,
                TransactionTypeGuid = Guid.NewGuid(),
                NameAR = "تحويل كاش",
                NameEN = "Cash Transfer",
                TransactionTypeID = 9,
                UserId = _UserId,
            },
            new TransactionType()
            {
                CreateDate = new DateTime(2020, 9, 1, 12, 00, 00),
                IsDeleted = false,
                TransactionTypeGuid = Guid.NewGuid(),
                NameAR = "شحن محفظة",
                NameEN = "Charge Wallet",
                TransactionTypeID = 10,
                UserId = _UserId,
            }
            );
        }
        public static void AddDeliverySetting()
        {
            _modelBuilder.Entity<DeliverySetting>().HasData(
              new DeliverySetting()
              {
                  DeliverySettingID = 1,
                  DeliverySettingGuid = Guid.NewGuid(),
                  BaseFare = 20,
                  CreateDate = new DateTime(2020, 9, 1, 12, 00, 00),
                  IsDeleted = false,
                  UserId = _UserId,
                  DriverCommision = 15,
                  IsEnable = true,
                  OverKmFare = 5,
                  MinKM = 3,
                  Tax = 0,
              });
        }
        #endregion
        #region Permissions
        private static void AddPermissionController()
        {
            _modelBuilder.Entity<PermissionController>().HasData(
                new PermissionController()
                {
                    PermissionControllerID = 1,
                    PermissionControllerGuid = Guid.NewGuid(),
                    PermissionControllerNameAr = "الصلاحيات",
                    PermissionControllerNameEn = "Permission",
                },
            new PermissionController()
            {
                PermissionControllerID = 2,
                PermissionControllerGuid = Guid.NewGuid(),
                PermissionControllerNameAr = "الدور",
                PermissionControllerNameEn = "Role",
            },
                new PermissionController()
                {
                    PermissionControllerID = 3,
                    PermissionControllerGuid = Guid.NewGuid(),
                    PermissionControllerNameAr = "الدولة",
                    PermissionControllerNameEn = "Country",
                },
                new PermissionController()
                {
                    PermissionControllerID = 4,
                    PermissionControllerGuid = Guid.NewGuid(),
                    PermissionControllerNameAr = "المنطقة",
                    PermissionControllerNameEn = "Region",
                },
                new PermissionController()
                {
                    PermissionControllerID = 5,
                    PermissionControllerGuid = Guid.NewGuid(),
                    PermissionControllerNameAr = "المدينة",
                    PermissionControllerNameEn = "City",
                },
                new PermissionController()
                {
                    PermissionControllerID = 6,
                    PermissionControllerGuid = Guid.NewGuid(),
                    PermissionControllerNameAr = "الوظائف",
                    PermissionControllerNameEn = "Jobs",
                },
                new PermissionController()
                {
                    PermissionControllerID = 7,
                    PermissionControllerGuid = Guid.NewGuid(),
                    PermissionControllerNameAr = "الاقسام",
                    PermissionControllerNameEn = "Departments",
                },
                new PermissionController()
                {
                    PermissionControllerID = 8,
                    PermissionControllerGuid = Guid.NewGuid(),
                    PermissionControllerNameAr = "الموظفين",
                    PermissionControllerNameEn = "Employees",
                },
                new PermissionController()
                {
                    PermissionControllerID = 9,
                    PermissionControllerGuid = Guid.NewGuid(),
                    PermissionControllerNameAr = "الجنسية",
                    PermissionControllerNameEn = "Nationality",
                },
                new PermissionController()
                {
                    PermissionControllerID = 10,
                    PermissionControllerGuid = Guid.NewGuid(),
                    PermissionControllerNameAr = "اعدادات الدور",
                    PermissionControllerNameEn = "Role Configuration",
                },
                new PermissionController()
                {
                    PermissionControllerID = 11,
                    PermissionControllerGuid = Guid.NewGuid(),
                    PermissionControllerNameAr = "الاعدادات",
                    PermissionControllerNameEn = "Configuration",
                },
                new PermissionController()
                {
                    PermissionControllerID = 12,
                    PermissionControllerGuid = Guid.NewGuid(),
                    PermissionControllerNameAr = "البنوك",
                    PermissionControllerNameEn = "Bank",
                },
                new PermissionController()
                {
                    PermissionControllerID = 13,
                    PermissionControllerGuid = Guid.NewGuid(),
                    PermissionControllerNameAr = "طرق الدفع",
                    PermissionControllerNameEn = "PaymentWay",
                },
                new PermissionController()
                {
                    PermissionControllerID = 14,
                    PermissionControllerGuid = Guid.NewGuid(),
                    PermissionControllerNameAr = "الأنشطة",
                    PermissionControllerNameEn = "Activity",
                },
                new PermissionController()
                {
                    PermissionControllerID = 15,
                    PermissionControllerGuid = Guid.NewGuid(),
                    PermissionControllerNameAr = "الأحياء",
                    PermissionControllerNameEn = "Block",
                },
                new PermissionController()
                {
                    PermissionControllerID = 16,
                    PermissionControllerGuid = Guid.NewGuid(),
                    PermissionControllerNameAr = "باقات التجار",
                    PermissionControllerNameEn = "Package",
                }
                ,
                new PermissionController()
                {
                    PermissionControllerID = 17,
                    PermissionControllerGuid = Guid.NewGuid(),
                    PermissionControllerNameAr = "حالات الدفع",
                    PermissionControllerNameEn = "PaymentStatus",
                }
                ,
                 //new PermissionController()
                 //{
                 //    PermissionControllerID = 106,
                 //    PermissionControllerGuid = Guid.NewGuid(),
                 //    PermissionControllerNameAr = "العملاء",
                 //    PermissionControllerNameEn = "Customer",
                 //},
                 //,
                 //new PermissionController()
                 //{
                 //    PermissionControllerID = 12,
                 //    PermissionControllerGuid = Guid.NewGuid(),
                 //    PermissionControllerNameAr = "المريض",
                 //    PermissionControllerNameEn = "Patient",
                 //},
                 //new PermissionController()
                 //{
                 //    PermissionControllerID = 13,
                 //    PermissionControllerGuid = Guid.NewGuid(),
                 //    PermissionControllerNameAr = "موظفين المستشفى",
                 //    PermissionControllerNameEn = "EntitiesEmployees",
                 //},
                 // new PermissionController()
                 // {
                 //     PermissionControllerID = 14,
                 //     PermissionControllerGuid = Guid.NewGuid(),
                 //     PermissionControllerNameAr = "سجل المواعيد",
                 //     PermissionControllerNameEn = "Appointments",
                 // },
                 //  new PermissionController()
                 //  {
                 //      PermissionControllerID = 15,
                 //      PermissionControllerGuid = Guid.NewGuid(),
                 //      PermissionControllerNameAr = "الأمراض",
                 //      PermissionControllerNameEn = "Diseases",
                 //  },
                 //   new PermissionController()
                 //   {
                 //       PermissionControllerID = 16,
                 //       PermissionControllerGuid = Guid.NewGuid(),
                 //       PermissionControllerNameAr = "الأدوية",
                 //       PermissionControllerNameEn = "Medicines",
                 //   },
                 //    new PermissionController()
                 //    {
                 //        PermissionControllerID = 17,
                 //        PermissionControllerGuid = Guid.NewGuid(),
                 //        PermissionControllerNameAr = "انواع الملفات",
                 //        PermissionControllerNameEn = "Files Types",
                 //    },
                 //     new PermissionController()
                 //     {
                 //         PermissionControllerID = 18,
                 //         PermissionControllerGuid = Guid.NewGuid(),
                 //         PermissionControllerNameAr = "الجنسية",
                 //         PermissionControllerNameEn = "Nationality",
                 //     },
                 //     new PermissionController()
                 //     {
                 //         PermissionControllerID = 19,
                 //         PermissionControllerGuid = Guid.NewGuid(),
                 //         PermissionControllerNameAr = "صفحات الأدوار",
                 //         PermissionControllerNameEn = "Role Configration",
                 //     },
                 //      new PermissionController()
                 //      {
                 //          PermissionControllerID = 20,
                 //          PermissionControllerGuid = Guid.NewGuid(),
                 //          PermissionControllerNameAr = "الكشوفات",
                 //          PermissionControllerNameEn = "Examination Status",
                 //      },
                 //       new PermissionController()
                 //       {
                 //           PermissionControllerID = 21,
                 //           PermissionControllerGuid = Guid.NewGuid(),
                 //           PermissionControllerNameAr = "تحت الملاحظة",
                 //           PermissionControllerNameEn = "Under Observation",
                 //       },
                 //        new PermissionController()
                 //        {
                 //            PermissionControllerID = 22,
                 //            PermissionControllerGuid = Guid.NewGuid(),
                 //            PermissionControllerNameAr = "الزيارات",
                 //            PermissionControllerNameEn = "Visits",
                 //        },
                 //         new PermissionController()
                 //         {
                 //             PermissionControllerID = 23,
                 //             PermissionControllerGuid = Guid.NewGuid(),
                 //             PermissionControllerNameAr = "انواع المستشفيات",
                 //             PermissionControllerNameEn = "Entity Type",
                 //         },
                 //          new PermissionController()
                 //          {
                 //              PermissionControllerID = 24,
                 //              PermissionControllerGuid = Guid.NewGuid(),
                 //              PermissionControllerNameAr = "حجز المواعيد",
                 //              PermissionControllerNameEn = "Add Appointment",
                 //          },
                 //          new PermissionController()
                 //          {
                 //              PermissionControllerID = 25,
                 //              PermissionControllerGuid = Guid.NewGuid(),
                 //              PermissionControllerNameAr = "الاطباء",
                 //              PermissionControllerNameEn = "Doctors",
                 //          },
                 //          new PermissionController()
                 //          {
                 //              PermissionControllerID = 26,
                 //              PermissionControllerGuid = Guid.NewGuid(),
                 //              PermissionControllerNameAr = "الادارة الطبية",
                 //              PermissionControllerNameEn = "Employees",
                 //          }

                 new PermissionController()
                 {
                     PermissionControllerID = 18,
                     PermissionControllerGuid = Guid.NewGuid(),
                     PermissionControllerNameAr = "اعدادت الشركه",
                     PermissionControllerNameEn = " Company Configuration",
                 },

                  new PermissionController()
                  {
                      PermissionControllerID = 19,
                      PermissionControllerGuid = Guid.NewGuid(),
                      PermissionControllerNameAr = "اعدادت الدفع",
                      PermissionControllerNameEn = "Payment Configuration",
                  },
                  new PermissionController()
                  {
                      PermissionControllerID = 20,
                      PermissionControllerGuid = Guid.NewGuid(),
                      PermissionControllerNameAr = "التصنيفات الرئسيه",
                      PermissionControllerNameEn = "Main categories",
                  },
                  new PermissionController()
                  {
                      PermissionControllerID = 21,
                      PermissionControllerGuid = Guid.NewGuid(),
                      PermissionControllerNameAr = "البنرات",
                      PermissionControllerNameEn = "Sliders",
                  },

                  new PermissionController()
                  {
                      PermissionControllerID = 22,
                      PermissionControllerGuid = Guid.NewGuid(),
                      PermissionControllerNameAr = "شركات الشحن",
                      PermissionControllerNameEn = "Shipping Company",
                  },
                   new PermissionController()
                   {
                       PermissionControllerID = 23,
                       PermissionControllerGuid = Guid.NewGuid(),
                       PermissionControllerNameAr = "الاستفسارات",
                       PermissionControllerNameEn = "Inqueries",
                   },
                  new PermissionController()
                  {
                      PermissionControllerID = 24,
                      PermissionControllerGuid = Guid.NewGuid(),
                      PermissionControllerNameAr = "الصفحات الرئيسية",
                      PermissionControllerNameEn = "Main Pages",
                  },
                   new PermissionController()
                   {
                       PermissionControllerID = 25,
                       PermissionControllerGuid = Guid.NewGuid(),
                       PermissionControllerNameAr = "المتاجر",
                       PermissionControllerNameEn = "Vendors",
                   },
                   new PermissionController()
                   {
                       PermissionControllerID = 26,
                       PermissionControllerGuid = Guid.NewGuid(),
                       PermissionControllerNameAr = "الزبائن",
                       PermissionControllerNameEn = "Customer",
                   },
                    new PermissionController()
                    {
                        PermissionControllerID = 27,
                        PermissionControllerGuid = Guid.NewGuid(),
                        PermissionControllerNameAr = "المنتجات",
                        PermissionControllerNameEn = "Product",
                    }
                    ,
                    new PermissionController()
                    {
                        PermissionControllerID = 28,
                        PermissionControllerGuid = Guid.NewGuid(),
                        PermissionControllerNameAr = "أنواع العناوين",
                        PermissionControllerNameEn = "Address Types",
                    }
                     ,
                    new PermissionController()
                    {
                        PermissionControllerID = 29,
                        PermissionControllerGuid = Guid.NewGuid(),
                        PermissionControllerNameAr = "الكلمات المفتاحية",
                        PermissionControllerNameEn = "KeyWords",
                    },
                     new PermissionController()
                     {
                         PermissionControllerID = 30,
                         PermissionControllerGuid = Guid.NewGuid(),
                         PermissionControllerNameAr = "منتجات الاوبريشن",
                         PermissionControllerNameEn = "Product Operation",
                     },
                     new PermissionController()
                     {
                         PermissionControllerID = 31,
                         PermissionControllerGuid = Guid.NewGuid(),
                         PermissionControllerNameAr = "طلبات الاوبريشن",
                         PermissionControllerNameEn = "Operation Orders",
                     },
                     new PermissionController()
                     {
                         PermissionControllerID = 32,
                         PermissionControllerGuid = Guid.NewGuid(),
                         PermissionControllerNameAr = "طلبات المتجر",
                         PermissionControllerNameEn = "Vendor Orders",
                     },
                     new PermissionController()
                     {
                         PermissionControllerID = 33,
                         PermissionControllerGuid = Guid.NewGuid(),
                         PermissionControllerNameAr = "الفروع",
                         PermissionControllerNameEn = "Branches",
                     },
                     new PermissionController()
                     {
                         PermissionControllerID = 34,
                         PermissionControllerGuid = Guid.NewGuid(),
                         PermissionControllerNameAr = "اكواد الخصم",
                         PermissionControllerNameEn = "Promo Code",
                     },
                     new PermissionController()
                     {
                         PermissionControllerID = 35,
                         PermissionControllerGuid = Guid.NewGuid(),
                         PermissionControllerNameAr = "سجل الاجازات",
                         PermissionControllerNameEn = "Vac History",
                     },
                     new PermissionController()
                     {
                         PermissionControllerID = 36,
                         PermissionControllerGuid = Guid.NewGuid(),
                         PermissionControllerNameAr = "سجل ايقاف الاشتراكات",
                         PermissionControllerNameEn = "Enable History",
                     },
                      new PermissionController()
                      {
                          PermissionControllerID = 37,
                          PermissionControllerGuid = Guid.NewGuid(),
                          PermissionControllerNameAr = "الطلبات الجديده للسائق",
                          PermissionControllerNameEn = "Driver New Requests",
                      },
                     new PermissionController()
                     {
                         PermissionControllerID = 38,
                         PermissionControllerGuid = Guid.NewGuid(),
                         PermissionControllerNameAr = "الطلبات الملغية للسائق",
                         PermissionControllerNameEn = "Driver Rejected Requests",
                     },
                     new PermissionController()
                     {
                         PermissionControllerID = 39,
                         PermissionControllerGuid = Guid.NewGuid(),
                         PermissionControllerNameAr = "طلبات انتظار التفعيل للسائق",
                         PermissionControllerNameEn = "Driver Waiting Activation",
                     },
                     new PermissionController()
                     {
                         PermissionControllerID = 40,
                         PermissionControllerGuid = Guid.NewGuid(),
                         PermissionControllerNameAr = "طلبات تحت المراجعة للسائق",
                         PermissionControllerNameEn = "Driver Under Requests",
                     },
                      new PermissionController()
                      {
                          PermissionControllerID = 41,
                          PermissionControllerGuid = Guid.NewGuid(),
                          PermissionControllerNameAr = "الفواتير",
                          PermissionControllerNameEn = "Invoices",
                      }
                      ,
                      new PermissionController()
                      {
                          PermissionControllerID = 42,
                          PermissionControllerGuid = Guid.NewGuid(),
                          PermissionControllerNameAr = "فواتير المتجر",
                          PermissionControllerNameEn = "Store Invoice",
                      },
                      new PermissionController()
                      {
                          PermissionControllerID = 43,
                          PermissionControllerGuid = Guid.NewGuid(),
                          PermissionControllerNameAr = "الخصم و الضريبة",
                          PermissionControllerNameEn = "Vat Discount",
                      },
                      new PermissionController()
                      {
                          PermissionControllerID = 44,
                          PermissionControllerGuid = Guid.NewGuid(),
                          PermissionControllerNameAr = "ماليات الكباتن",
                          PermissionControllerNameEn = "Captain Financial",
                      },
                        new PermissionController()
                        {
                            PermissionControllerID = 45,
                            PermissionControllerGuid = Guid.NewGuid(),
                            PermissionControllerNameAr = "ارسال رسالة",
                            PermissionControllerNameEn = "Send Message",
                        },
                        new PermissionController()
                        {
                            PermissionControllerID = 46,
                            PermissionControllerGuid = Guid.NewGuid(),
                            PermissionControllerNameAr = "المدن المغطاة",
                            PermissionControllerNameEn = "Cities Covered",
                        },
                        new PermissionController()
                        {
                            PermissionControllerID = 47,
                            PermissionControllerGuid = Guid.NewGuid(),
                            PermissionControllerNameAr = "مناطق الكابتن",
                            PermissionControllerNameEn = "Captain Zone",
                        },
            new PermissionController()
            {
                PermissionControllerID = 48,
                PermissionControllerGuid = Guid.NewGuid(),
                PermissionControllerNameAr = "تعيين سائق",
                PermissionControllerNameEn = "Assign Driver",
            },
            new PermissionController()
            {
                PermissionControllerID = 49,
                PermissionControllerGuid = Guid.NewGuid(),
                PermissionControllerNameAr = "عملاء المتاجر",
                PermissionControllerNameEn = "Vendor Customer",
            },
             new PermissionController()
             {
                 PermissionControllerID = 50,
                 PermissionControllerGuid = Guid.NewGuid(),
                 PermissionControllerNameAr = "سجل اجازات المتاجر",
                 PermissionControllerNameEn = "Vendor Vac History",
             },
              new PermissionController()
              {
                  PermissionControllerID = 51,
                  PermissionControllerGuid = Guid.NewGuid(),
                  PermissionControllerNameAr = "سجل ايقاف اشتراكات المتاجر",
                  PermissionControllerNameEn = "Vendor Enable History",
              },
               new PermissionController()
               {
                   PermissionControllerID = 52,
                   PermissionControllerGuid = Guid.NewGuid(),
                   PermissionControllerNameAr = "اسئلة المنتج",
                   PermissionControllerNameEn = "Product Question",
               },
                new PermissionController()
                {
                    PermissionControllerID = 53,
                    PermissionControllerGuid = Guid.NewGuid(),
                    PermissionControllerNameAr = "تقييم الطلب",
                    PermissionControllerNameEn = "Order Rate",
                },
                  new PermissionController()
                  {
                      PermissionControllerID = 54,
                      PermissionControllerGuid = Guid.NewGuid(),
                      PermissionControllerNameAr = "اسئلة المنتج للمتجر",
                      PermissionControllerNameEn = "Vendor Product Question",
                  },
                new PermissionController()
                {
                    PermissionControllerID = 55,
                    PermissionControllerGuid = Guid.NewGuid(),
                    PermissionControllerNameAr = "تقييم الطلب للمتجر",
                    PermissionControllerNameEn = "Vendor Order Rate",
                },
                  new PermissionController()
                  {
                      PermissionControllerID = 56,
                      PermissionControllerGuid = Guid.NewGuid(),
                      PermissionControllerNameAr = "الطلبات المجدولة للاوبريشن",
                      PermissionControllerNameEn = "Operation Schedule Orders",
                  },
                   new PermissionController()
                   {
                       PermissionControllerID = 57,
                       PermissionControllerGuid = Guid.NewGuid(),
                       PermissionControllerNameAr = "الطلبات المجدولة للمتجر",
                       PermissionControllerNameEn = "Vendor Schedule Orders",
                   },
                    new PermissionController()
                    {
                        PermissionControllerID = 58,
                        PermissionControllerGuid = Guid.NewGuid(),
                        PermissionControllerNameAr = "الرئيسية للاوبريشن",
                        PermissionControllerNameEn = "Operation Dashboard",
                    },
                     new PermissionController()
                     {
                         PermissionControllerID = 59,
                         PermissionControllerGuid = Guid.NewGuid(),
                         PermissionControllerNameAr = "الرئيسية للمتجر",
                         PermissionControllerNameEn = "Vendor Dashboard",
                     },
                      new PermissionController()
                      {
                          PermissionControllerID = 60,
                          PermissionControllerGuid = Guid.NewGuid(),
                          PermissionControllerNameAr = "استفسارات الكباتن",
                          PermissionControllerNameEn = "Driver Support",
                      },
                      new PermissionController()
                      {
                          PermissionControllerID = 61,
                          PermissionControllerGuid = Guid.NewGuid(),
                          PermissionControllerNameAr = "بريد الاشتراكات",
                          PermissionControllerNameEn = "Subscribes",
                      },
                      new PermissionController()
                      {
                          PermissionControllerID = 62,
                          PermissionControllerGuid = Guid.NewGuid(),
                          PermissionControllerNameAr = "رصيد العميل",
                          PermissionControllerNameEn = "Customer Balance",
                      }

            );
        }
        private static void AddPermissionAction()
        {
            _modelBuilder.Entity<PermissionAction>().HasData(
                new PermissionAction()
                {
                    PermissionActionID = 1,
                    PermissionActionGuid = Guid.NewGuid(),
                    PermissionActionNameAr = "عرض",
                    PermissionActionNameEn = "View",
                },
                new PermissionAction()
                {
                    PermissionActionID = 2,
                    PermissionActionGuid = Guid.NewGuid(),
                    PermissionActionNameAr = "اضافة",
                    PermissionActionNameEn = "Insert",
                },
                new PermissionAction()
                {
                    PermissionActionID = 3,
                    PermissionActionGuid = Guid.NewGuid(),
                    PermissionActionNameAr = "تعديل",
                    PermissionActionNameEn = "Update",
                },
                new PermissionAction()
                {
                    PermissionActionID = 4,
                    PermissionActionGuid = Guid.NewGuid(),
                    PermissionActionNameAr = "حذف",
                    PermissionActionNameEn = "Delete",
                }

            );
        }
        private static void AddPermissionControllerAction()
        {
            _modelBuilder.Entity<PermissionControllerAction>().HasData(
                //============================Permission == الصلاحية======================================
                new PermissionControllerAction() { PermissionControllerActionID = 1, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 1, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 2, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 1, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 3, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 1, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 4, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 1, PermissionActionID = 4 }, // Delete
             //============================Role == الدور======================================
                new PermissionControllerAction() { PermissionControllerActionID = 5, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 2, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 6, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 2, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 7, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 2, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 8, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 2, PermissionActionID = 4 }, // Delete
             //============================Country == الدولة======================================
                new PermissionControllerAction() { PermissionControllerActionID = 9, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 3, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 10, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 3, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 11, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 3, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 12, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 3, PermissionActionID = 4 }, // Delete
              //============================Region == المنطقة======================================
                new PermissionControllerAction() { PermissionControllerActionID = 13, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 4, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 14, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 4, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 15, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 4, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 16, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 4, PermissionActionID = 4 }, // Delete
              //============================City == المدينة======================================
                new PermissionControllerAction() { PermissionControllerActionID = 17, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 5, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 18, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 5, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 19, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 5, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 20, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 5, PermissionActionID = 4 }, // Delete
              //============================Jobs == الوظائف======================================
                new PermissionControllerAction() { PermissionControllerActionID = 21, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 6, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 22, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 6, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 23, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 6, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 24, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 6, PermissionActionID = 4 }, // Delete
              //============================Departments == الاقسام======================================
                new PermissionControllerAction() { PermissionControllerActionID = 25, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 7, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 26, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 7, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 27, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 7, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 28, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 7, PermissionActionID = 4 }, // Delete
              //============================Employees == الموظفين======================================
                new PermissionControllerAction() { PermissionControllerActionID = 29, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 8, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 30, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 8, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 31, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 8, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 32, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 8, PermissionActionID = 4 }, // Delete
              //============================Nationality == الجنسية======================================
                new PermissionControllerAction() { PermissionControllerActionID = 33, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 9, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 34, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 9, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 35, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 9, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 36, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 9, PermissionActionID = 4 }, // Delete
              //============================RoleConfigration == اعدادات الدور======================================
                new PermissionControllerAction() { PermissionControllerActionID = 37, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 10, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 38, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 10, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 39, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 10, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 40, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 10, PermissionActionID = 4 }, // Delete
               //============================Configuration == الاعدادات======================================
                new PermissionControllerAction() { PermissionControllerActionID = 41, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 11, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 42, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 11, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 43, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 11, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 44, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 11, PermissionActionID = 4 }, // Delet

                new PermissionControllerAction() { PermissionControllerActionID = 45, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 12, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 46, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 12, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 47, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 12, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 48, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 12, PermissionActionID = 4 },// Delete
              //============================Payment ways == طرق الدفع======================================                                                 
                new PermissionControllerAction() { PermissionControllerActionID = 49, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 13, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 50, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 13, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 51, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 13, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 52, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 13, PermissionActionID = 4 }, // Delete
               //============================Activity == الأنشطة======================================
                new PermissionControllerAction() { PermissionControllerActionID = 53, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 14, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 54, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 14, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 55, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 14, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 56, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 14, PermissionActionID = 4 }, // Delete
               //============================Block == الاحياء======================================
                new PermissionControllerAction() { PermissionControllerActionID = 57, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 15, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 58, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 15, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 59, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 15, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 60, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 15, PermissionActionID = 4 },// Delete

                //============================Package == الحزم======================================
                new PermissionControllerAction() { PermissionControllerActionID = 61, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 16, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 62, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 16, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 63, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 16, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 64, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 16, PermissionActionID = 4 }, // Delete
               //============================حالات الدفع  == Payment Status======================================
                new PermissionControllerAction() { PermissionControllerActionID = 65, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 17, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 66, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 17, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 67, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 17, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 68, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 17, PermissionActionID = 4 }, // Delete

                new PermissionControllerAction() { PermissionControllerActionID = 69, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 18, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 70, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 18, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 71, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 18, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 72, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 18, PermissionActionID = 4 }, // Delete
               ////============================Diseases == الأمراض======================================                                                                                                                                                                         //============================Appointments ==  المواعيد======================================
                new PermissionControllerAction() { PermissionControllerActionID = 73, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 19, PermissionActionID = 1 }, // view
                new PermissionControllerAction() { PermissionControllerActionID = 74, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 19, PermissionActionID = 2 }, // create
                new PermissionControllerAction() { PermissionControllerActionID = 75, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 19, PermissionActionID = 3 }, // update
                new PermissionControllerAction() { PermissionControllerActionID = 76, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 19, PermissionActionID = 4 }, // delete
               ////============================Diseases == الأمراض===============7======================
                new PermissionControllerAction() { PermissionControllerActionID = 77, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 20, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 78, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 20, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 79, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 20, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 80, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 20, PermissionActionID = 4 }, // Delete
               ////============================Medicines ==  الأدوية=============7========================
                new PermissionControllerAction() { PermissionControllerActionID = 81, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 21, PermissionActionID = 1 }, // view
                new PermissionControllerAction() { PermissionControllerActionID = 82, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 21, PermissionActionID = 2 }, // create
                new PermissionControllerAction() { PermissionControllerActionID = 83, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 21, PermissionActionID = 3 }, // update
                new PermissionControllerAction() { PermissionControllerActionID = 84, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 21, PermissionActionID = 4 }, // delete

                     new PermissionControllerAction() { PermissionControllerActionID = 85, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 22, PermissionActionID = 1 }, // view
                new PermissionControllerAction() { PermissionControllerActionID = 86, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 22, PermissionActionID = 2 }, // create
                new PermissionControllerAction() { PermissionControllerActionID = 87, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 22, PermissionActionID = 3 }, // update
                new PermissionControllerAction() { PermissionControllerActionID = 88, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 22, PermissionActionID = 4 }, // delete

                     new PermissionControllerAction() { PermissionControllerActionID = 89, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 23, PermissionActionID = 1 }, // view
                new PermissionControllerAction() { PermissionControllerActionID = 90, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 23, PermissionActionID = 2 }, // create
                new PermissionControllerAction() { PermissionControllerActionID = 91, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 23, PermissionActionID = 3 }, // update
                new PermissionControllerAction() { PermissionControllerActionID = 92, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 23, PermissionActionID = 4 }, // delete

                     new PermissionControllerAction() { PermissionControllerActionID = 93, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 24, PermissionActionID = 1 }, // view
                new PermissionControllerAction() { PermissionControllerActionID = 94, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 24, PermissionActionID = 2 }, // create
                new PermissionControllerAction() { PermissionControllerActionID = 95, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 24, PermissionActionID = 3 }, // update
                new PermissionControllerAction() { PermissionControllerActionID = 96, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 24, PermissionActionID = 4 }, // delete
               //============================Vendors == المتاجر======================================
                new PermissionControllerAction() { PermissionControllerActionID = 97, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 25, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 98, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 25, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 99, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 25, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 100, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 25, PermissionActionID = 4 }, // Delete
                //============================Customer == الزبائن======================================
                new PermissionControllerAction() { PermissionControllerActionID = 101, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 26, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 102, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 26, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 103, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 26, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 104, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 26, PermissionActionID = 4 }, // Delete
                //============================Product == المنتجات======================================
                new PermissionControllerAction() { PermissionControllerActionID = 105, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 27, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 106, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 27, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 107, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 27, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 108, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 27, PermissionActionID = 4 } // Delete
                , // Delete
                  //============================AddressTypes == أنواع العناوين======================================
                new PermissionControllerAction() { PermissionControllerActionID = 109, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 28, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 110, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 28, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 111, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 28, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 112, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 28, PermissionActionID = 4 } // Delete
                                , // Delete
                                  //============================KeyWords == الكلمات المفتاحية ======================================
                new PermissionControllerAction() { PermissionControllerActionID = 113, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 29, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 114, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 29, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 115, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 29, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 116, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 29, PermissionActionID = 4 }, // Delete
                //============================ProductOperation == منتجات الاوبريشن ======================================
                new PermissionControllerAction() { PermissionControllerActionID = 117, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 30, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 118, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 30, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 119, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 30, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 120, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 30, PermissionActionID = 4 }, // Delete
                //============================Operation Orders == طلبات الاوبريشن ======================================
                new PermissionControllerAction() { PermissionControllerActionID = 121, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 31, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 122, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 31, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 123, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 31, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 124, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 31, PermissionActionID = 4 }, // Delete
                //============================Vendor Orders == طلبات المتجر ======================================
                new PermissionControllerAction() { PermissionControllerActionID = 125, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 32, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 126, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 32, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 127, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 32, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 128, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 32, PermissionActionID = 4 }, // Delete
                //============================Branches == الافرع ======================================
                new PermissionControllerAction() { PermissionControllerActionID = 129, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 33, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 130, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 33, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 131, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 33, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 132, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 33, PermissionActionID = 4 }, // Delete

                new PermissionControllerAction() { PermissionControllerActionID = 133, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 34, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 134, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 34, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 135, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 34, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 136, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 34, PermissionActionID = 4 }, // Delete

                new PermissionControllerAction() { PermissionControllerActionID = 137, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 35, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 138, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 35, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 139, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 35, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 140, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 35, PermissionActionID = 4 }, // Delete

                new PermissionControllerAction() { PermissionControllerActionID = 141, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 36, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 142, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 36, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 143, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 36, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 144, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 36, PermissionActionID = 4 }, // Delete
                //============================Driver New Requests == الطلبات الجديدة للسائق ======================================
                new PermissionControllerAction() { PermissionControllerActionID = 145, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 37, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 146, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 37, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 147, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 37, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 148, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 37, PermissionActionID = 4 }, // Delete
                //============================Driver Rejected Requests == الطلبات المرفوضة للسائق ======================================
                new PermissionControllerAction() { PermissionControllerActionID = 149, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 38, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 150, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 38, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 151, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 38, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 152, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 38, PermissionActionID = 4 }, // Delete
                //============================Driver Waiting Activation == الطلبات قيد التفعيل للسائق ======================================
                new PermissionControllerAction() { PermissionControllerActionID = 153, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 39, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 154, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 39, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 155, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 39, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 156, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 39, PermissionActionID = 4 }, // Delete
                //============================Driver Under Requests == الطلبات تحت المراجعة للسائق ======================================
                new PermissionControllerAction() { PermissionControllerActionID = 157, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 40, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 158, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 40, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 159, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 40, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 160, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 40, PermissionActionID = 4 }, // Delete
                    new PermissionControllerAction() { PermissionControllerActionID = 161, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 41, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 162, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 41, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 163, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 41, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 164, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 41, PermissionActionID = 4 }, // Delete
                      new PermissionControllerAction() { PermissionControllerActionID = 165, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 42, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 166, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 42, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 167, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 42, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 168, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 42, PermissionActionID = 4 },
                new PermissionControllerAction() { PermissionControllerActionID = 169, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 43, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 170, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 43, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 171, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 43, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 172, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 43, PermissionActionID = 4 },// Delete
                   
                new PermissionControllerAction() { PermissionControllerActionID = 173, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 44, PermissionActionID = 1 }, // View
                new PermissionControllerAction() { PermissionControllerActionID = 174, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 44, PermissionActionID = 2 }, // Create
                new PermissionControllerAction() { PermissionControllerActionID = 175, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 44, PermissionActionID = 3 }, // Update
                new PermissionControllerAction() { PermissionControllerActionID = 176, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 44, PermissionActionID = 4 },
               //============================SendMessage == ارسال رسالة======================================
               new PermissionControllerAction() { PermissionControllerActionID = 177, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 45, PermissionActionID = 1 }, // View
               new PermissionControllerAction() { PermissionControllerActionID = 178, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 45, PermissionActionID = 2 }, // Create
               new PermissionControllerAction() { PermissionControllerActionID = 179, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 45, PermissionActionID = 3 }, // Update
               new PermissionControllerAction() { PermissionControllerActionID = 180, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 45, PermissionActionID = 4 }, // Delete
               //============================CitiesCovered ==  المدن المغطاه======================================
               new PermissionControllerAction() { PermissionControllerActionID = 181, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 46, PermissionActionID = 1 }, // View
               new PermissionControllerAction() { PermissionControllerActionID = 182, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 46, PermissionActionID = 2 }, // Create
               new PermissionControllerAction() { PermissionControllerActionID = 183, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 46, PermissionActionID = 3 }, // Update
               new PermissionControllerAction() { PermissionControllerActionID = 184, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 46, PermissionActionID = 4 }, // Delete
               //============================CaptainZone == مناطق الكابتن======================================
               new PermissionControllerAction() { PermissionControllerActionID = 185, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 47, PermissionActionID = 1 }, // View
               new PermissionControllerAction() { PermissionControllerActionID = 186, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 47, PermissionActionID = 2 }, // Create
               new PermissionControllerAction() { PermissionControllerActionID = 187, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 47, PermissionActionID = 3 }, // Update
               new PermissionControllerAction() { PermissionControllerActionID = 188, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 47, PermissionActionID = 4 }, // Delete
               //============================AssignDriver ==  تعيين سائق======================================
               new PermissionControllerAction() { PermissionControllerActionID = 189, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 48, PermissionActionID = 1 }, // View
               new PermissionControllerAction() { PermissionControllerActionID = 190, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 48, PermissionActionID = 2 }, // Create
               new PermissionControllerAction() { PermissionControllerActionID = 191, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 48, PermissionActionID = 3 }, // Update
               new PermissionControllerAction() { PermissionControllerActionID = 192, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 48, PermissionActionID = 4 }, // Delete
               //============================VendorCustomer ==  عملاء المتاجر======================================
               new PermissionControllerAction() { PermissionControllerActionID = 193, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 49, PermissionActionID = 1 }, // View
               new PermissionControllerAction() { PermissionControllerActionID = 194, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 49, PermissionActionID = 2 }, // Create
               new PermissionControllerAction() { PermissionControllerActionID = 195, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 49, PermissionActionID = 3 }, // Update
               new PermissionControllerAction() { PermissionControllerActionID = 196, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 49, PermissionActionID = 4 }, // Delete
               //============================VendorVacHistory ==  سجل اجازات المتاجر======================================
               new PermissionControllerAction() { PermissionControllerActionID = 197, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 50, PermissionActionID = 1 }, // View
               new PermissionControllerAction() { PermissionControllerActionID = 198, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 50, PermissionActionID = 2 }, // Create
               new PermissionControllerAction() { PermissionControllerActionID = 199, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 50, PermissionActionID = 3 }, // Update
               new PermissionControllerAction() { PermissionControllerActionID = 200, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 50, PermissionActionID = 4 }, // Delete
               //============================VendorEnableHistory ==  سجل ايقاف اشتراكات المتاجر======================================
               new PermissionControllerAction() { PermissionControllerActionID = 201, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 51, PermissionActionID = 1 }, // View
               new PermissionControllerAction() { PermissionControllerActionID = 202, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 51, PermissionActionID = 2 }, // Create
               new PermissionControllerAction() { PermissionControllerActionID = 203, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 51, PermissionActionID = 3 }, // Update
               new PermissionControllerAction() { PermissionControllerActionID = 204, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 51, PermissionActionID = 4 }, // Delete
               //============================ProductQuestion ==  اسئلة المنتج======================================
               new PermissionControllerAction() { PermissionControllerActionID = 205, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 52, PermissionActionID = 1 }, // View
               new PermissionControllerAction() { PermissionControllerActionID = 206, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 52, PermissionActionID = 2 }, // Create
               new PermissionControllerAction() { PermissionControllerActionID = 207, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 52, PermissionActionID = 3 }, // Update
               new PermissionControllerAction() { PermissionControllerActionID = 208, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 52, PermissionActionID = 4 }, // Delete
               //============================OrderRate ==  تقييم الطلب =====================================
               new PermissionControllerAction() { PermissionControllerActionID = 209, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 53, PermissionActionID = 1 }, // View
               new PermissionControllerAction() { PermissionControllerActionID = 210, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 53, PermissionActionID = 2 }, // Create
               new PermissionControllerAction() { PermissionControllerActionID = 211, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 53, PermissionActionID = 3 }, // Update
               new PermissionControllerAction() { PermissionControllerActionID = 212, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 53, PermissionActionID = 4 }, // Delete
               //============================VendorProductQuestion ==  اسئلة المنتج للمتجر======================================
               new PermissionControllerAction() { PermissionControllerActionID = 213, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 54, PermissionActionID = 1 }, // View
               new PermissionControllerAction() { PermissionControllerActionID = 214, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 54, PermissionActionID = 2 }, // Create
               new PermissionControllerAction() { PermissionControllerActionID = 215, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 54, PermissionActionID = 3 }, // Update
               new PermissionControllerAction() { PermissionControllerActionID = 216, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 54, PermissionActionID = 4 }, // Delete
               //============================VendorOrderRate ==  تقييم الطلب للمنتج======================================
               new PermissionControllerAction() { PermissionControllerActionID = 217, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 55, PermissionActionID = 1 }, // View
               new PermissionControllerAction() { PermissionControllerActionID = 218, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 55, PermissionActionID = 2 }, // Create
               new PermissionControllerAction() { PermissionControllerActionID = 219, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 55, PermissionActionID = 3 }, // Update
               new PermissionControllerAction() { PermissionControllerActionID = 220, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 55, PermissionActionID = 4 }, // Delete
               //============================OperationScheduleOrders ==  الطلبات المجدولة للاوبريشن======================================
               new PermissionControllerAction() { PermissionControllerActionID = 221, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 56, PermissionActionID = 1 }, // View
               new PermissionControllerAction() { PermissionControllerActionID = 222, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 56, PermissionActionID = 2 }, // Create
               new PermissionControllerAction() { PermissionControllerActionID = 223, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 56, PermissionActionID = 3 }, // Update
               new PermissionControllerAction() { PermissionControllerActionID = 224, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 56, PermissionActionID = 4 }, // Delete
               //============================VendorScheduleOrders == الطلبات المجدولة للمتجر======================================
               new PermissionControllerAction() { PermissionControllerActionID = 225, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 57, PermissionActionID = 1 }, // View
               new PermissionControllerAction() { PermissionControllerActionID = 226, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 57, PermissionActionID = 2 }, // Create
               new PermissionControllerAction() { PermissionControllerActionID = 227, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 57, PermissionActionID = 3 }, // Update
               new PermissionControllerAction() { PermissionControllerActionID = 228, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 57, PermissionActionID = 4 }, // Delete
                //============================OperationDashboard ==  الرئيسية للاوبريشن======================================
               new PermissionControllerAction() { PermissionControllerActionID = 229, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 58, PermissionActionID = 1 }, // View
               new PermissionControllerAction() { PermissionControllerActionID = 230, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 58, PermissionActionID = 2 }, // Create
               new PermissionControllerAction() { PermissionControllerActionID = 231, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 58, PermissionActionID = 3 }, // Update
               new PermissionControllerAction() { PermissionControllerActionID = 232, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 58, PermissionActionID = 4 }, // Delete
               //============================VendorDashboard == الرئيسية للمتجر======================================
               new PermissionControllerAction() { PermissionControllerActionID = 233, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 59, PermissionActionID = 1 }, // View
               new PermissionControllerAction() { PermissionControllerActionID = 234, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 59, PermissionActionID = 2 }, // Create
               new PermissionControllerAction() { PermissionControllerActionID = 235, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 59, PermissionActionID = 3 }, // Update
               new PermissionControllerAction() { PermissionControllerActionID = 236, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 59, PermissionActionID = 4 }, // Delete
               //============================DriverSupport == استفسارات الكباتن======================================
               new PermissionControllerAction() { PermissionControllerActionID = 237, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 60, PermissionActionID = 1 }, // View
               new PermissionControllerAction() { PermissionControllerActionID = 238, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 60, PermissionActionID = 2 }, // Create
               new PermissionControllerAction() { PermissionControllerActionID = 239, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 60, PermissionActionID = 3 }, // Update
               new PermissionControllerAction() { PermissionControllerActionID = 240, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 60, PermissionActionID = 4 }, // Delete
               //============================Subscribe == بريد الاشتراكات======================================
               new PermissionControllerAction() { PermissionControllerActionID = 241, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 61, PermissionActionID = 1 }, // View
               new PermissionControllerAction() { PermissionControllerActionID = 242, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 61, PermissionActionID = 2 }, // Create
               new PermissionControllerAction() { PermissionControllerActionID = 243, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 61, PermissionActionID = 3 }, // Update
               new PermissionControllerAction() { PermissionControllerActionID = 244, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 61, PermissionActionID = 4 }, // Delete
                //============================CustomerBalance == رصيد العميل======================================
               new PermissionControllerAction() { PermissionControllerActionID = 245, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 62, PermissionActionID = 1 }, // View
               new PermissionControllerAction() { PermissionControllerActionID = 246, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 62, PermissionActionID = 2 }, // Create
               new PermissionControllerAction() { PermissionControllerActionID = 247, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 62, PermissionActionID = 3 }, // Update
               new PermissionControllerAction() { PermissionControllerActionID = 248, PermissionControllerActionGuid = Guid.NewGuid(), PermissionControllerID = 62, PermissionActionID = 4 } // Delete

);
        }
        private static void AddCustomRole()
        {
            _modelBuilder.Entity<CustomRole>().HasData(
                new CustomRole()
                {
                    Id = 1,
                    Name = "Admin",
                    RoleTypeId = 1
                },
                new CustomRole()
                {
                    Id = 2,
                    Name = "Vendor",
                    RoleTypeId = 2
                },
                new CustomRole()
                {
                    Id = 3,
                    Name = "Customer",
                    RoleTypeId = 2
                },
                new CustomRole()
                {
                    Id = 4,
                    Name = "Employee",
                    RoleTypeId = 2
                }
            );
        }
        private static void AddRoleConfig()
        {
            _modelBuilder.Entity<RoleConfig>().HasData(
            #region Admin_System
            new RoleConfig()
            {
                RoleConfigID = 1,
                PermissionControllerActionID = 1,
                RoleId = 1
            },
           new RoleConfig()
           {
               RoleConfigID = 2,
               PermissionControllerActionID = 2,
               RoleId = 1
           },
            new RoleConfig()
            {
                RoleConfigID = 3,
                PermissionControllerActionID = 3,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 4,
                PermissionControllerActionID = 4,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 5,
                PermissionControllerActionID = 5,
                RoleId = 1
            },
           new RoleConfig()
           {
               RoleConfigID = 6,
               PermissionControllerActionID = 6,
               RoleId = 1
           },
            new RoleConfig()
            {
                RoleConfigID = 7,
                PermissionControllerActionID = 7,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 8,
                PermissionControllerActionID = 8,
                RoleId = 1
            },
             new RoleConfig()
             {
                 RoleConfigID = 9,
                 PermissionControllerActionID = 9,
                 RoleId = 1
             },
           new RoleConfig()
           {
               RoleConfigID = 10,
               PermissionControllerActionID = 10,
               RoleId = 1
           },
            new RoleConfig()
            {
                RoleConfigID = 11,
                PermissionControllerActionID = 11,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 12,
                PermissionControllerActionID = 12,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 13,
                PermissionControllerActionID = 13,
                RoleId = 1
            },
           new RoleConfig()
           {
               RoleConfigID = 14,
               PermissionControllerActionID = 14,
               RoleId = 1
           },
            new RoleConfig()
            {
                RoleConfigID = 15,
                PermissionControllerActionID = 15,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 16,
                PermissionControllerActionID = 16,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 17,
                PermissionControllerActionID = 17,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 18,
                PermissionControllerActionID = 18,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 19,
                PermissionControllerActionID = 19,
                RoleId = 1
            },
           new RoleConfig()
           {
               RoleConfigID = 20,
               PermissionControllerActionID = 20,
               RoleId = 1
           },
            new RoleConfig()
            {
                RoleConfigID = 21,
                PermissionControllerActionID = 21,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 22,
                PermissionControllerActionID = 22,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 23,
                PermissionControllerActionID = 23,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 24,
                PermissionControllerActionID = 24,
                RoleId = 1
            },
           new RoleConfig()
           {
               RoleConfigID = 25,
               PermissionControllerActionID = 25,
               RoleId = 1
           },
            new RoleConfig()
            {
                RoleConfigID = 26,
                PermissionControllerActionID = 26,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 27,
                PermissionControllerActionID = 27,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 28,
                PermissionControllerActionID = 28,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 29,
                PermissionControllerActionID = 29,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 30,
                PermissionControllerActionID = 30,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 31,
                PermissionControllerActionID = 31,
                RoleId = 1
            },
           new RoleConfig()
           {
               RoleConfigID = 32,
               PermissionControllerActionID = 32,
               RoleId = 1
           },
            new RoleConfig()
            {
                RoleConfigID = 33,
                PermissionControllerActionID = 33,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 34,
                PermissionControllerActionID = 34,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 35,
                PermissionControllerActionID = 35,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 36,
                PermissionControllerActionID = 36,
                RoleId = 1
            },
           new RoleConfig()
           {
               RoleConfigID = 37,
               PermissionControllerActionID = 37,
               RoleId = 1
           },
            new RoleConfig()
            {
                RoleConfigID = 38,
                PermissionControllerActionID = 38,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 39,
                PermissionControllerActionID = 39,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 40,
                PermissionControllerActionID = 40,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 41,
                PermissionControllerActionID = 41,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 42,
                PermissionControllerActionID = 42,
                RoleId = 1
            },
           new RoleConfig()
           {
               RoleConfigID = 43,
               PermissionControllerActionID = 43,
               RoleId = 1
           },
            new RoleConfig()
            {
                RoleConfigID = 44,
                PermissionControllerActionID = 44,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 45,
                PermissionControllerActionID = 45,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 46,
                PermissionControllerActionID = 46,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 47,
                PermissionControllerActionID = 47,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 48,
                PermissionControllerActionID = 48,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 49,
                PermissionControllerActionID = 49,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 50,
                PermissionControllerActionID = 50,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 51,
                PermissionControllerActionID = 51,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 52,
                PermissionControllerActionID = 52,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 53,
                PermissionControllerActionID = 53,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 54,
                PermissionControllerActionID = 54,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 55,
                PermissionControllerActionID = 55,
                RoleId = 1
            },
            new RoleConfig()
            {
                RoleConfigID = 56,
                PermissionControllerActionID = 56,
                RoleId = 1
            }, new RoleConfig()
            {
                RoleConfigID = 57,
                PermissionControllerActionID = 57,
                RoleId = 1
            }, new RoleConfig()
            {
                RoleConfigID = 58,
                PermissionControllerActionID = 58,
                RoleId = 1
            }, new RoleConfig()
            {
                RoleConfigID = 59,
                PermissionControllerActionID = 59,
                RoleId = 1
            }, new RoleConfig()
            {
                RoleConfigID = 60,
                PermissionControllerActionID = 60,
                RoleId = 1
            }, new RoleConfig()
            {
                RoleConfigID = 61,
                PermissionControllerActionID = 61,
                RoleId = 1
            }, new RoleConfig()
            {
                RoleConfigID = 62,
                PermissionControllerActionID = 62,
                RoleId = 1
            }, new RoleConfig()
            {
                RoleConfigID = 63,
                PermissionControllerActionID = 63,
                RoleId = 1
            }, new RoleConfig()
            {
                RoleConfigID = 64,
                PermissionControllerActionID = 64,
                RoleId = 1
            }, new RoleConfig()
            {
                RoleConfigID = 65,
                PermissionControllerActionID = 65,
                RoleId = 1
            }, new RoleConfig()
            {
                RoleConfigID = 66,
                PermissionControllerActionID = 66,
                RoleId = 1
            }, new RoleConfig()
            {
                RoleConfigID = 67,
                PermissionControllerActionID = 67,
                RoleId = 1
            }, new RoleConfig()
            {
                RoleConfigID = 68,
                PermissionControllerActionID = 68,
                RoleId = 1
            }, new RoleConfig()
            {
                RoleConfigID = 69,
                PermissionControllerActionID = 69,
                RoleId = 1
            }, new RoleConfig()
            {
                RoleConfigID = 70,
                PermissionControllerActionID = 70,
                RoleId = 1
            }, new RoleConfig()
            {
                RoleConfigID = 71,
                PermissionControllerActionID = 71,
                RoleId = 1
            }, new RoleConfig()
            {
                RoleConfigID = 72,
                PermissionControllerActionID = 72,
                RoleId = 1
            }, new RoleConfig()
            {
                RoleConfigID = 73,
                PermissionControllerActionID = 73,
                RoleId = 1
            }, new RoleConfig()
            {
                RoleConfigID = 74,
                PermissionControllerActionID = 74,
                RoleId = 1
            }, new RoleConfig()
            {
                RoleConfigID = 75,
                PermissionControllerActionID = 75,
                RoleId = 1
            }, new RoleConfig()
            {
                RoleConfigID = 76,
                PermissionControllerActionID = 76,
                RoleId = 1
            },
             new RoleConfig()
             {
                 RoleConfigID = 77,
                 PermissionControllerActionID = 77,
                 RoleId = 1
             },
              new RoleConfig()
              {
                  RoleConfigID = 78,
                  PermissionControllerActionID = 78,
                  RoleId = 1
              },
               new RoleConfig()
               {
                   RoleConfigID = 79,
                   PermissionControllerActionID = 79,
                   RoleId = 1
               },
                new RoleConfig()
                {
                    RoleConfigID = 80,
                    PermissionControllerActionID = 80,
                    RoleId = 1
                },
                 new RoleConfig()
                 {
                     RoleConfigID = 81,
                     PermissionControllerActionID = 81,
                     RoleId = 1
                 },
                  new RoleConfig()
                  {
                      RoleConfigID = 82,
                      PermissionControllerActionID = 82,
                      RoleId = 1
                  },
                   new RoleConfig()
                   {
                       RoleConfigID = 83,
                       PermissionControllerActionID = 83,
                       RoleId = 1
                   },
                    new RoleConfig()
                    {
                        RoleConfigID = 84,
                        PermissionControllerActionID = 84,
                        RoleId = 1
                    },
                      new RoleConfig()
                      {
                          RoleConfigID = 85,
                          PermissionControllerActionID = 85,
                          RoleId = 1
                      },
               new RoleConfig()
               {
                   RoleConfigID = 86,
                   PermissionControllerActionID = 86,
                   RoleId = 1
               },
                new RoleConfig()
                {
                    RoleConfigID = 87,
                    PermissionControllerActionID = 87,
                    RoleId = 1
                },
                 new RoleConfig()
                 {
                     RoleConfigID = 88,
                     PermissionControllerActionID = 88,
                     RoleId = 1
                 },
                  new RoleConfig()
                  {
                      RoleConfigID = 89,
                      PermissionControllerActionID = 89,
                      RoleId = 1
                  },
                   new RoleConfig()
                   {
                       RoleConfigID = 90,
                       PermissionControllerActionID = 90,
                       RoleId = 1
                   },
                    new RoleConfig()
                    {
                        RoleConfigID = 91,
                        PermissionControllerActionID = 91,
                        RoleId = 1
                    },
                      new RoleConfig()
                      {
                          RoleConfigID = 92,
                          PermissionControllerActionID = 92,
                          RoleId = 1
                      },
               new RoleConfig()
               {
                   RoleConfigID = 93,
                   PermissionControllerActionID = 93,
                   RoleId = 1
               },
                new RoleConfig()
                {
                    RoleConfigID = 94,
                    PermissionControllerActionID = 94,
                    RoleId = 1
                },
                 new RoleConfig()
                 {
                     RoleConfigID = 95,
                     PermissionControllerActionID = 95,
                     RoleId = 1
                 },
                  new RoleConfig()
                  {
                      RoleConfigID = 96,
                      PermissionControllerActionID = 96,
                      RoleId = 1
                  },
                   new RoleConfig()
                   {
                       RoleConfigID = 97,
                       PermissionControllerActionID = 97,
                       RoleId = 1
                   },
                    new RoleConfig()
                    {
                        RoleConfigID = 98,
                        PermissionControllerActionID = 98,
                        RoleId = 1
                    },
                      new RoleConfig()
                      {
                          RoleConfigID = 99,
                          PermissionControllerActionID = 99,
                          RoleId = 1
                      },
               new RoleConfig()
               {
                   RoleConfigID = 100,
                   PermissionControllerActionID = 100,
                   RoleId = 1
               },
                new RoleConfig()
                {
                    RoleConfigID = 101,
                    PermissionControllerActionID = 101,
                    RoleId = 1
                },
                 new RoleConfig()
                 {
                     RoleConfigID = 102,
                     PermissionControllerActionID = 102,
                     RoleId = 1
                 },
                  new RoleConfig()
                  {
                      RoleConfigID = 103,
                      PermissionControllerActionID = 103,
                      RoleId = 1
                  },
                   new RoleConfig()
                   {
                       RoleConfigID = 104,
                       PermissionControllerActionID = 104,
                       RoleId = 1
                   },
                 new RoleConfig()
                 {
                     RoleConfigID = 109,
                     PermissionControllerActionID = 109,
                     RoleId = 1
                 },
                  new RoleConfig()
                  {
                      RoleConfigID = 110,
                      PermissionControllerActionID = 110,
                      RoleId = 1
                  },
                   new RoleConfig()
                   {
                       RoleConfigID = 111,
                       PermissionControllerActionID = 111,
                       RoleId = 1
                   },
                    new RoleConfig()
                    {
                        RoleConfigID = 112,
                        PermissionControllerActionID = 112,
                        RoleId = 1
                    },
                      new RoleConfig()
                      {
                          RoleConfigID = 113,
                          PermissionControllerActionID = 113,
                          RoleId = 1
                      },
               new RoleConfig()
               {
                   RoleConfigID = 114,
                   PermissionControllerActionID = 114,
                   RoleId = 1
               },
                new RoleConfig()
                {
                    RoleConfigID = 115,
                    PermissionControllerActionID = 115,
                    RoleId = 1
                },
                 new RoleConfig()
                 {
                     RoleConfigID = 116,
                     PermissionControllerActionID = 116,
                     RoleId = 1
                 },
                  new RoleConfig()
                  {
                      RoleConfigID = 117,
                      PermissionControllerActionID = 117,
                      RoleId = 1
                  },
                   new RoleConfig()
                   {
                       RoleConfigID = 118,
                       PermissionControllerActionID = 118,
                       RoleId = 1
                   },
                    new RoleConfig()
                    {
                        RoleConfigID = 119,
                        PermissionControllerActionID = 119,
                        RoleId = 1
                    },
                     new RoleConfig()
                     {
                         RoleConfigID = 120,
                         PermissionControllerActionID = 120,
                         RoleId = 1
                     },
                      new RoleConfig()
                      {
                          RoleConfigID = 121,
                          PermissionControllerActionID = 121,
                          RoleId = 1
                      },
                   new RoleConfig()
                   {
                       RoleConfigID = 122,
                       PermissionControllerActionID = 122,
                       RoleId = 1
                   },
                   new RoleConfig()
                    {
                        RoleConfigID = 123,
                        PermissionControllerActionID = 123,
                        RoleId = 1
                    },
                   new RoleConfig()
                     {
                         RoleConfigID = 124,
                         PermissionControllerActionID = 124,
                         RoleId = 1
                     },
                   new RoleConfig()
                     {
                         RoleConfigID = 129,
                         PermissionControllerActionID = 129,
                         RoleId = 1
                     },
                   new RoleConfig()
                   {
                       RoleConfigID = 130,
                       PermissionControllerActionID = 130,
                       RoleId = 1
                   },
                   new RoleConfig()
                    {
                        RoleConfigID = 131,
                        PermissionControllerActionID = 131,
                        RoleId = 1
                    },
                     new RoleConfig()
                     {
                         RoleConfigID = 132,
                         PermissionControllerActionID = 132,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 133,
                         PermissionControllerActionID = 133,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 134,
                         PermissionControllerActionID = 134,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 135,
                         PermissionControllerActionID = 135,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 136,
                         PermissionControllerActionID = 136,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 137,
                         PermissionControllerActionID = 137,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 138,
                         PermissionControllerActionID = 138,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 139,
                         PermissionControllerActionID = 139,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 140,
                         PermissionControllerActionID = 140,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 141,
                         PermissionControllerActionID = 141,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 142,
                         PermissionControllerActionID = 142,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 143,
                         PermissionControllerActionID = 143,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 144,
                         PermissionControllerActionID = 144,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 145,
                         PermissionControllerActionID = 145,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 146,
                         PermissionControllerActionID = 146,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 147,
                         PermissionControllerActionID = 147,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 148,
                         PermissionControllerActionID = 148,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 149,
                         PermissionControllerActionID = 149,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 150,
                         PermissionControllerActionID = 150,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 151,
                         PermissionControllerActionID = 151,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 152,
                         PermissionControllerActionID = 152,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 153,
                         PermissionControllerActionID = 153,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 154,
                         PermissionControllerActionID = 154,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 155,
                         PermissionControllerActionID = 155,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 156,
                         PermissionControllerActionID = 156,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 157,
                         PermissionControllerActionID = 157,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 158,
                         PermissionControllerActionID = 158,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 159,
                         PermissionControllerActionID = 159,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 160,
                         PermissionControllerActionID = 160,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 161,
                         PermissionControllerActionID = 161,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 162,
                         PermissionControllerActionID = 162,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 163,
                         PermissionControllerActionID = 163,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 164,
                         PermissionControllerActionID = 164,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 169,
                         PermissionControllerActionID = 169,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 170,
                         PermissionControllerActionID = 170,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 171,
                         PermissionControllerActionID = 171,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 172,
                         PermissionControllerActionID = 172,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 173,
                         PermissionControllerActionID = 173,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 174,
                         PermissionControllerActionID = 174,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 175,
                         PermissionControllerActionID = 175,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 176,
                         PermissionControllerActionID = 176,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 178,
                         PermissionControllerActionID = 178,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 179,
                         PermissionControllerActionID = 179,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 180,
                         PermissionControllerActionID = 180,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 181,
                         PermissionControllerActionID = 181,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 182,
                         PermissionControllerActionID = 182,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 183,
                         PermissionControllerActionID = 183,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 184,
                         PermissionControllerActionID = 184,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 185,
                         PermissionControllerActionID = 185,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 186,
                         PermissionControllerActionID = 186,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 187,
                         PermissionControllerActionID = 187,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 188,
                         PermissionControllerActionID = 188,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 189,
                         PermissionControllerActionID = 189,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 190,
                         PermissionControllerActionID = 190,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 191,
                         PermissionControllerActionID = 191,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 192,
                         PermissionControllerActionID = 192,
                         RoleId = 1
                     },
                      new RoleConfig()
                      {
                          RoleConfigID = 205,
                          PermissionControllerActionID = 205,
                          RoleId = 1
                      },
                     new RoleConfig()
                     {
                         RoleConfigID = 206,
                         PermissionControllerActionID = 206,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 207,
                         PermissionControllerActionID = 207,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 208,
                         PermissionControllerActionID = 208,
                         RoleId = 1
                     },
                      new RoleConfig()
                      {
                          RoleConfigID = 209,
                          PermissionControllerActionID = 209,
                          RoleId = 1
                      },
                     new RoleConfig()
                     {
                         RoleConfigID = 210,
                         PermissionControllerActionID = 210,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 211,
                         PermissionControllerActionID = 211,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 212,
                         PermissionControllerActionID = 212,
                         RoleId = 1
                     },
                      new RoleConfig()
                      {
                          RoleConfigID = 221,
                          PermissionControllerActionID = 221,
                          RoleId = 1
                      },
                     new RoleConfig()
                     {
                         RoleConfigID = 222,
                         PermissionControllerActionID = 222,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 223,
                         PermissionControllerActionID = 223,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 224,
                         PermissionControllerActionID = 224,
                         RoleId = 1
                     },
                      new RoleConfig()
                      {
                          RoleConfigID = 229,
                          PermissionControllerActionID = 229,
                          RoleId = 1
                      },
                     new RoleConfig()
                     {
                         RoleConfigID = 230,
                         PermissionControllerActionID = 230,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 231,
                         PermissionControllerActionID = 231,
                         RoleId = 1
                     },
                     new RoleConfig()
                     {
                         RoleConfigID = 232,
                         PermissionControllerActionID = 232,
                         RoleId = 1
                     },
                 new RoleConfig()
                 {
                     RoleConfigID = 237,
                     PermissionControllerActionID = 237,
                     RoleId = 1
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 238,
                     PermissionControllerActionID = 238,
                     RoleId = 1
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 239,
                     PermissionControllerActionID = 239,
                     RoleId = 1
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 240,
                     PermissionControllerActionID = 240,
                     RoleId = 1
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 241,
                     PermissionControllerActionID = 241,
                     RoleId = 1
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 242,
                     PermissionControllerActionID = 242,
                     RoleId = 1
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 243,
                     PermissionControllerActionID = 243,
                     RoleId = 1
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 244,
                     PermissionControllerActionID = 244,
                     RoleId = 1
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 245,
                     PermissionControllerActionID = 245,
                     RoleId = 1
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 246,
                     PermissionControllerActionID = 246,
                     RoleId = 1
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 247,
                     PermissionControllerActionID = 247,
                     RoleId = 1
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 248,
                     PermissionControllerActionID = 248,
                     RoleId = 1
                 },
            #endregion
            #region Vendor
            new RoleConfig()
           {
               RoleConfigID = 105,
               PermissionControllerActionID = 105,
               RoleId = 2
           },
            new RoleConfig()
                      {
                          RoleConfigID = 106,
                          PermissionControllerActionID = 106,
                          RoleId = 2
                      },
            new RoleConfig()
               {
                   RoleConfigID = 107,
                   PermissionControllerActionID = 107,
                   RoleId = 2
               },
            new RoleConfig()
                {
                    RoleConfigID = 108,
                    PermissionControllerActionID = 108,
                    RoleId = 2
                },
            new RoleConfig()
                  {
                      RoleConfigID = 125,
                      PermissionControllerActionID = 125,
                      RoleId = 2
                  },
            new RoleConfig()
                      {
                          RoleConfigID = 126,
                          PermissionControllerActionID = 126,
                          RoleId = 2
                      },
            new RoleConfig()
               {
                   RoleConfigID = 127,
                   PermissionControllerActionID = 127,
                   RoleId = 2
               },
            new RoleConfig()
                {
                    RoleConfigID = 128,
                    PermissionControllerActionID = 128,
                    RoleId = 2
                },
                 new RoleConfig()
                 {
                     RoleConfigID = 165,
                     PermissionControllerActionID = 165,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 166,
                     PermissionControllerActionID = 166,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 167,
                     PermissionControllerActionID = 167,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 168,
                     PermissionControllerActionID = 168,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 193,
                     PermissionControllerActionID = 193,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 194,
                     PermissionControllerActionID = 194,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 195,
                     PermissionControllerActionID = 195,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 196,
                     PermissionControllerActionID = 196,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 197,
                     PermissionControllerActionID = 197,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 198,
                     PermissionControllerActionID = 198,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 199,
                     PermissionControllerActionID = 199,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 200,
                     PermissionControllerActionID = 200,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 201,
                     PermissionControllerActionID = 201,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 202,
                     PermissionControllerActionID = 202,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 203,
                     PermissionControllerActionID = 203,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 204,
                     PermissionControllerActionID = 204,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 213,
                     PermissionControllerActionID = 213,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 214,
                     PermissionControllerActionID = 214,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 215,
                     PermissionControllerActionID = 215,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 216,
                     PermissionControllerActionID = 216,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 217,
                     PermissionControllerActionID = 217,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 218,
                     PermissionControllerActionID = 218,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 219,
                     PermissionControllerActionID = 219,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 220,
                     PermissionControllerActionID = 220,
                     RoleId = 2
                 },
                  new RoleConfig()
                  {
                      RoleConfigID = 225,
                      PermissionControllerActionID = 225,
                      RoleId = 2
                  },
                 new RoleConfig()
                 {
                     RoleConfigID = 226,
                     PermissionControllerActionID = 226,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 227,
                     PermissionControllerActionID = 227,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 228,
                     PermissionControllerActionID = 228,
                     RoleId = 2
                 },
                  new RoleConfig()
                  {
                      RoleConfigID = 233,
                      PermissionControllerActionID = 233,
                      RoleId = 2
                  },
                 new RoleConfig()
                 {
                     RoleConfigID = 234,
                     PermissionControllerActionID = 234,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 235,
                     PermissionControllerActionID = 235,
                     RoleId = 2
                 },
                 new RoleConfig()
                 {
                     RoleConfigID = 236,
                     PermissionControllerActionID = 236,
                     RoleId = 2
                 }

                 #endregion
                 #region Customer

                 #endregion
                 #region Employees_Entity

                 #endregion
              );
        }
        //private static void AddMasterBrunch()
        //{
        //    _modelBuilder.Entity<Branches>().HasData(new Branches()
        //    {
        //        BranchesGuid = Guid.Parse("7c17102c-6661-496c-8317-dd5bc2bade5d"),
        //        BranchesID = 1,
        //        CreateDate = new DateTime(2020, 9, 1, 12, 00, 00),
        //        NameAR = "الفرع الرئيسي",
        //        BranchesManger = "",
        //        ClientID = 1,
        //        IsDeleted = false,
        //        MobileNo = "",
        //        Address = "",
        //        NameEn = "Main Branch",
        //        UserId = _UserId,
        //        Email = "",
        //        Fax = "",
        //        WebSite = ""
        //    });
        //}

        #endregion
    }
}
