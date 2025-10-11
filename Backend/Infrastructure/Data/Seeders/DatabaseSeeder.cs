using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Seeders
{
    /// <summary>
    /// Seeds the database with sample data for testing
    /// Run this once after initial migration
    /// </summary>
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Check if database already has data
            if (await context.Users.AnyAsync())
            {
                Console.WriteLine("Database already seeded. Skipping...");
                return;
            }

            Console.WriteLine("Seeding database...");

            // 1. Create Addresses
            var addresses = new List<Address>
            {
                new Address
                {
                    Street = "123 Main Street",
                    City = "Los Angeles",
                    State = "California",
                    ZipCode = "90001",
                    Country = "USA",
                    Latitude = 34.0522m,
                    Longitude = -118.2437m
                },
                new Address
                {
                    Street = "456 Oak Avenue",
                    City = "San Diego",
                    State = "California",
                    ZipCode = "92101",
                    Country = "USA",
                    Latitude = 32.7157m,
                    Longitude = -117.1611m
                },
                new Address
                {
                    Street = "789 Beach Road",
                    City = "Santa Monica",
                    State = "California",
                    ZipCode = "90401",
                    Country = "USA",
                    Latitude = 34.0195m,
                    Longitude = -118.4912m
                },
                new Address
                {
                    Street = "321 Mountain View Dr",
                    City = "San Francisco",
                    State = "California",
                    ZipCode = "94102",
                    Country = "USA",
                    Latitude = 37.7749m,
                    Longitude = -122.4194m
                }
            };
            await context.Addresses.AddRangeAsync(addresses);
            await context.SaveChangesAsync();

            // 2. Create Users (Admin and Technicians)
            var users = new List<User>
            {
                new User
                {
                    Email = "admin@solarcr m.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    FirstName = "John",
                    LastName = "Administrator",
                    Role = UserRole.Admin,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Email = "mike.tech@solarcrm.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Tech123!"),
                    FirstName = "Mike",
                    LastName = "Technician",
                    Role = UserRole.Technician,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Specialization = "Solar Panel Installation",
                    LicenseNumber = "CA-SOLAR-12345"
                },
                new User
                {
                    Email = "sarah.tech@solarcrm.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Tech123!"),
                    FirstName = "Sarah",
                    LastName = "Engineer",
                    Role = UserRole.Technician,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Specialization = "Electrical Systems",
                    LicenseNumber = "CA-ELEC-67890"
                }
            };
            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();

            // 3. Create Customers
            var customers = new List<Customer>
            {
                new Customer
                {
                    FirstName = "Robert",
                    LastName = "Johnson",
                    Email = "robert.j@email.com",
                    PhoneNumber = "+1-555-0101",
                    Status = CustomerStatus.Active,
                    RegistrationDate = DateTime.UtcNow.AddMonths(-6),
                    LastActivityDate = DateTime.UtcNow.AddDays(-5),
                    ContactAddressId = addresses[0].Id
                },
                new Customer
                {
                    FirstName = "Emily",
                    LastName = "Davis",
                    Email = "emily.d@email.com",
                    PhoneNumber = "+1-555-0102",
                    Status = CustomerStatus.Prospect,
                    RegistrationDate = DateTime.UtcNow.AddMonths(-2),
                    LastActivityDate = DateTime.UtcNow.AddDays(-10),
                    ContactAddressId = addresses[1].Id
                },
                new Customer
                {
                    FirstName = "Michael",
                    LastName = "Brown",
                    Email = "michael.b@email.com",
                    PhoneNumber = "+1-555-0103",
                    Status = CustomerStatus.Lead,
                    RegistrationDate = DateTime.UtcNow.AddDays(-15),
                    LastActivityDate = DateTime.UtcNow.AddDays(-2),
                    ContactAddressId = addresses[2].Id
                }
            };
            await context.Customers.AddRangeAsync(customers);
            await context.SaveChangesAsync();

            // 4. Create Installations
            var installations = new List<Installation>
            {
                new Installation
                {
                    ProjectName = "Johnson Residence Solar Installation",
                    Status = InstallationStatus.Active,
                    StartDate = DateTime.UtcNow.AddMonths(-5),
                    CompletionDate = DateTime.UtcNow.AddMonths(-1),
                    SystemSizeKw = 8.5m,
                    PanelCount = 20,
                    InverterType = "SolarEdge SE7600H-US",
                    Notes = "Residential installation with battery backup",
                    CustomerId = customers[0].Id,
                    InstallationAddressId = addresses[0].Id,
                    Customer = customers[0],
                    InstallationAddress = addresses[0]
                },
                new Installation
                {
                    ProjectName = "Davis Beach House Solar Project",
                    Status = InstallationStatus.Installation,
                    StartDate = DateTime.UtcNow.AddMonths(-1),
                    SystemSizeKw = 6.0m,
                    PanelCount = 15,
                    InverterType = "Enphase IQ7+",
                    Notes = "Installation in progress",
                    CustomerId = customers[1].Id,
                    InstallationAddressId = addresses[3].Id,
                    Customer = customers[1],
                    InstallationAddress = addresses[3]
                },
                new Installation
                {
                    ProjectName = "Brown Residence - Initial Survey",
                    Status = InstallationStatus.Survey,
                    StartDate = DateTime.UtcNow.AddDays(-10),
                    SystemSizeKw = 5.5m,
                    PanelCount = 12,
                    Notes = "Awaiting site survey completion",
                    CustomerId = customers[2].Id,
                    InstallationAddressId = addresses[2].Id,
                    Customer = customers[2],
                    InstallationAddress = addresses[2]
                }
            };
            await context.Installations.AddRangeAsync(installations);
            await context.SaveChangesAsync();

            // 5. Create Installation Status History
            var statusHistories = new List<InstallationStatusHistory>
            {
                // For Installation 1 (Johnson - Active)
                new InstallationStatusHistory
                {
                    InstallationId = installations[0].Id,
                    FromStatus = InstallationStatus.Survey,
                    ToStatus = InstallationStatus.Design,
                    ChangedAt = DateTime.UtcNow.AddMonths(-5),
                    ChangedByUserId = users[0].Id,
                    Notes = "Site survey completed successfully",
                    Installation = installations[0],
                    ChangedBy = users[0]
                },
                new InstallationStatusHistory
                {
                    InstallationId = installations[0].Id,
                    FromStatus = InstallationStatus.Design,
                    ToStatus = InstallationStatus.Permits,
                    ChangedAt = DateTime.UtcNow.AddMonths(-4).AddDays(-20),
                    ChangedByUserId = users[0].Id,
                    Notes = "System design approved by customer",
                    Installation = installations[0],
                    ChangedBy = users[0]
                },
                new InstallationStatusHistory
                {
                    InstallationId = installations[0].Id,
                    FromStatus = InstallationStatus.Permits,
                    ToStatus = InstallationStatus.Installation,
                    ChangedAt = DateTime.UtcNow.AddMonths(-3),
                    ChangedByUserId = users[0].Id,
                    Notes = "All permits obtained from city",
                    Installation = installations[0],
                    ChangedBy = users[0]
                },
                new InstallationStatusHistory
                {
                    InstallationId = installations[0].Id,
                    FromStatus = InstallationStatus.Installation,
                    ToStatus = InstallationStatus.Inspection,
                    ChangedAt = DateTime.UtcNow.AddMonths(-2),
                    ChangedByUserId = users[1].Id,
                    Notes = "Installation completed",
                    Installation = installations[0],
                    ChangedBy = users[0]
                },
                new InstallationStatusHistory
                {
                    InstallationId = installations[0].Id,
                    FromStatus = InstallationStatus.Inspection,
                    ToStatus = InstallationStatus.Active,
                    ChangedAt = DateTime.UtcNow.AddMonths(-1),
                    ChangedByUserId = users[0].Id,
                    Notes = "Passed final inspection, system activated",
                    Installation = installations[0],
                    ChangedBy = users[0]
                },
                // For Installation 2 (Davis - In Progress)
                new InstallationStatusHistory
                {
                    InstallationId = installations[1].Id,
                    FromStatus = InstallationStatus.Survey,
                    ToStatus = InstallationStatus.Design,
                    ChangedAt = DateTime.UtcNow.AddDays(-25),
                    ChangedByUserId = users[0].Id,
                    Installation = installations[0],
                    ChangedBy = users[0]
                },
                new InstallationStatusHistory
                {
                    InstallationId = installations[1].Id,
                    FromStatus = InstallationStatus.Design,
                    ToStatus = InstallationStatus.Permits,
                    ChangedAt = DateTime.UtcNow.AddDays(-20),
                    ChangedByUserId = users[0].Id,
                    Installation = installations[1],
                    ChangedBy = users[0]
                },
                new InstallationStatusHistory
                {
                    InstallationId = installations[1].Id,
                    FromStatus = InstallationStatus.Permits,
                    ToStatus = InstallationStatus.Installation,
                    ChangedAt = DateTime.UtcNow.AddDays(-5),
                    ChangedByUserId = users[1].Id,
                    Notes = "Installation crew assigned",
                    Installation = installations[1],
                    ChangedBy = users[1]
                }
            };
            await context.InstallationStatusHistories.AddRangeAsync(statusHistories);
            await context.SaveChangesAsync();

            // 6. Create Technician Assignments
            var technicianAssignments = new List<InstallationTechnician>
            {
                new InstallationTechnician
                {
                    InstallationId = installations[0].Id,
                    TechnicianId = users[1].Id,
                    AssignedDate = DateTime.UtcNow.AddMonths(-5),
                    CompletedDate = DateTime.UtcNow.AddMonths(-1),
                    Role = "Lead Installer",
                    Notes = "Successfully completed installation",
                    Installation = installations[0],
                    Technician = users[1]
                },
                new InstallationTechnician
                {
                    InstallationId = installations[0].Id,
                    TechnicianId = users[2].Id,
                    AssignedDate = DateTime.UtcNow.AddMonths(-3),
                    CompletedDate = DateTime.UtcNow.AddMonths(-1),
                    Role = "Electrical Engineer",
                    Notes = "Handled electrical connections and grid integration",
                    Installation = installations[0],
                    Technician = users[2]
                },
                new InstallationTechnician
                {
                    InstallationId = installations[1].Id,
                    TechnicianId = users[1].Id,
                    AssignedDate = DateTime.UtcNow.AddDays(-5),
                    Role = "Lead Installer",
                    Notes = "Currently installing",
                    Installation = installations[1],
                    Technician = users[1]
                }
            };
            await context.InstallationTechnicians.AddRangeAsync(technicianAssignments);
            await context.SaveChangesAsync();

            // 7. Create Weather Data
            var weatherData = new List<WeatherData>();
            var random = new Random();
            var baseDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-30));
            
            for (int i = 0; i < 30; i++)
            {
                weatherData.Add(new WeatherData
                {
                    Date = baseDate.AddDays(i),
                    Location = "Los Angeles, CA",
                    Condition = random.Next(0, 3) switch
                    {
                        0 => "Sunny",
                        1 => "Partly Cloudy",
                        _ => "Cloudy"
                    },
                    TemperatureCelsius = 20m + random.Next(-5, 10),
                    CloudCoverPercentage = random.Next(0, 100)
                });
            }
            await context.WeatherData.AddRangeAsync(weatherData);
            await context.SaveChangesAsync();

            // 8. Create Energy Production Data (for active installation)
            var energyProductions = new List<EnergyProduction>();
            for (int i = 0; i < 30; i++)
            {
                var date = baseDate.AddDays(i);
                var weather = weatherData.First(w => w.Date == date);
                var baseProd = weather.Condition == "Sunny" ? 45m : weather.Condition == "Partly Cloudy" ? 35m : 25m;
                
                energyProductions.Add(new EnergyProduction
                {
                    InstallationId = installations[0].Id,
                    ProductionDate = date,
                    ExpectedProductionKwh = 40m,
                    ActualProductionKwh = baseProd +
                                          random.Next(-5,
                                              5),
                    HealthStatus = SystemHealthStatus.Excellent,
                    WeatherDataId = weather.Id,
                    Installation = installations[0]
                });
            }
            await context.EnergyProductions.AddRangeAsync(energyProductions);
            await context.SaveChangesAsync();

            // 9. Create Equipment
            var equipment = new List<Equipment>
            {
                // Equipment for Installation 1
                new Equipment
                {
                    Type = EquipmentType.SolarPanel,
                    Model = "LG NeON 2 LG350Q1C-A5",
                    SerialNumber = "SN-PANEL-001",
                    Manufacturer = "LG Electronics",
                    Status = EquipmentStatus.Installed,
                    PurchaseDate = DateTime.UtcNow.AddMonths(-6),
                    WarrantyExpiryDate = DateTime.UtcNow.AddYears(25),
                    Cost = 350m,
                    Specifications = "{\"wattage\": 350, \"efficiency\": 21.1, \"dimensions\": \"1686x1016x40mm\"}",
                    InstallationId = installations[0].Id
                },
                new Equipment
                {
                    Type = EquipmentType.Inverter,
                    Model = "SolarEdge SE7600H-US",
                    SerialNumber = "SN-INV-001",
                    Manufacturer = "SolarEdge",
                    Status = EquipmentStatus.Installed,
                    PurchaseDate = DateTime.UtcNow.AddMonths(-6),
                    WarrantyExpiryDate = DateTime.UtcNow.AddYears(12),
                    Cost = 1500m,
                    Specifications = "{\"capacity\": \"7.6kW\", \"efficiency\": 97.6, \"type\": \"String Inverter\"}",
                    InstallationId = installations[0].Id
                },
                // Equipment for Installation 2
                new Equipment
                {
                    Type = EquipmentType.SolarPanel,
                    Model = "Canadian Solar CS3W-400P",
                    SerialNumber = "SN-PANEL-002",
                    Manufacturer = "Canadian Solar",
                    Status = EquipmentStatus.Assigned,
                    PurchaseDate = DateTime.UtcNow.AddMonths(-2),
                    WarrantyExpiryDate = DateTime.UtcNow.AddYears(25),
                    Cost = 320m,
                    Specifications = "{\"wattage\": 400, \"efficiency\": 19.8}",
                    InstallationId = installations[1].Id
                },
                // Equipment in stock
                new Equipment
                {
                    Type = EquipmentType.Battery,
                    Model = "Tesla Powerwall 2",
                    SerialNumber = "SN-BAT-001",
                    Manufacturer = "Tesla",
                    Status = EquipmentStatus.InStock,
                    PurchaseDate = DateTime.UtcNow.AddMonths(-1),
                    WarrantyExpiryDate = DateTime.UtcNow.AddYears(10),
                    Cost = 7500m,
                    Specifications = "{\"capacity\": \"13.5kWh\", \"power\": \"5kW continuous\"}"
                },
                new Equipment
                {
                    Type = EquipmentType.MonitoringSystem,
                    Model = "SolarEdge Monitoring Portal",
                    SerialNumber = "SN-MON-001",
                    Manufacturer = "SolarEdge",
                    Status = EquipmentStatus.Installed,
                    PurchaseDate = DateTime.UtcNow.AddMonths(-6),
                    Cost = 300m,
                    Specifications = "{\"connectivity\": \"WiFi/Ethernet\", \"features\": \"Real-time monitoring\"}",
                    InstallationId = installations[0].Id
                }
            };
            await context.Equipment.AddRangeAsync(equipment);
            await context.SaveChangesAsync();

            // 10. Create Support Tickets
            var supportTickets = new List<SupportTicket>
            {
                new SupportTicket
                {
                    Title = "Inverter displaying error code",
                    Description =
                        "The inverter has been showing error code E012 for the past 2 days. System production has dropped significantly.",
                    Status = TicketStatus.InProgress,
                    Priority = TicketPriority.High,
                    CreatedAt = DateTime.UtcNow.AddDays(-3),
                    AssignedAt = DateTime.UtcNow.AddDays(-2),
                    CustomerId = customers[0].Id,
                    InstallationId = installations[0].Id,
                    AssignedToUserId = users[1].Id,
                    Customer = customers[0]
                },
                new SupportTicket
                {
                    Title = "Request for production report",
                    Description = "Could you please send me the detailed production report for last month? I need it for my utility company.",
                    Status = TicketStatus.Resolved,
                    Priority = TicketPriority.Low,
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    AssignedAt = DateTime.UtcNow.AddDays(-9),
                    ResolvedAt = DateTime.UtcNow.AddDays(-8),
                    ClosedAt = DateTime.UtcNow.AddDays(-7),
                    CustomerId = customers[0].Id,
                    InstallationId = installations[0].Id,
                    AssignedToUserId = users[0].Id,
                    ResolutionNotes = "Production report sent via email",
                    Customer = customers[0]
                },
                new SupportTicket
                {
                    Title = "Question about installation timeline",
                    Description = "When can we expect the installation to begin? We've been waiting for permits approval.",
                    Status = TicketStatus.Open,
                    Priority = TicketPriority.Medium,
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    CustomerId = customers[1].Id,
                    InstallationId = installations[1].Id,
                    Customer = customers[1]
                }
            };
            await context.SupportTickets.AddRangeAsync(supportTickets);
            await context.SaveChangesAsync();

            // 11. Create Documents
            var documents = new List<Document>
            {
                // Customer documents
                new Document
                {
                    FileName = "robert_johnson_id.pdf",
                    FilePath = "/uploads/customers/1/id_verification.pdf",
                    FileType = "pdf",
                    FileSize = 2048576,
                    Type = DocumentType.IdVerification,
                    UploadedAt = DateTime.UtcNow.AddMonths(-6),
                    Description = "Driver's license for identity verification",
                    CustomerId = customers[0].Id,
                    UploadedByUserId = users[0].Id,
                    UploadedBy = users[0]
                },
                new Document
                {
                    FileName = "robert_johnson_property_deed.pdf",
                    FilePath = "/uploads/customers/1/property_deed.pdf",
                    FileType = "pdf",
                    FileSize = 3145728,
                    Type = DocumentType.PropertyOwnership,
                    UploadedAt = DateTime.UtcNow.AddMonths(-6),
                    Description = "Property ownership documentation",
                    CustomerId = customers[0].Id,
                    UploadedByUserId = users[0].Id,
                    UploadedBy = users[0]
                },
                // Installation documents
                new Document
                {
                    FileName = "johnson_contract_signed.pdf",
                    FilePath = "/uploads/installations/1/contract.pdf",
                    FileType = "pdf",
                    FileSize = 1048576,
                    Type = DocumentType.InstallationContract,
                    UploadedAt = DateTime.UtcNow.AddMonths(-5),
                    Description = "Signed installation contract",
                    InstallationId = installations[0].Id,
                    UploadedByUserId = users[0].Id,
                    UploadedBy = users[0]
                },
                new Document
                {
                    FileName = "johnson_building_permit.pdf",
                    FilePath = "/uploads/installations/1/building_permit.pdf",
                    FileType = "pdf",
                    FileSize = 524288,
                    Type = DocumentType.BuildingPermit,
                    UploadedAt = DateTime.UtcNow.AddMonths(-4),
                    Description = "City building permit approval",
                    InstallationId = installations[0].Id,
                    UploadedByUserId = users[0].Id,
                    UploadedBy = users[0]
                },
                new Document
                {
                    FileName = "johnson_installation_photo1.jpg",
                    FilePath = "/uploads/installations/1/photo1.jpg",
                    FileType = "jpg",
                    FileSize = 4194304,
                    Type = DocumentType.InstallationPhoto,
                    UploadedAt = DateTime.UtcNow.AddMonths(-2),
                    Description = "Panels installed on roof - front view",
                    InstallationId = installations[0].Id,
                    UploadedByUserId = users[1].Id,
                    UploadedBy = users[1]
                },
                new Document
                {
                    FileName = "johnson_installation_photo2.jpg",
                    FilePath = "/uploads/installations/1/photo2.jpg",
                    FileType = "jpg",
                    FileSize = 3932160,
                    Type = DocumentType.InstallationPhoto,
                    UploadedAt = DateTime.UtcNow.AddMonths(-2),
                    Description = "Inverter and electrical connections",
                    InstallationId = installations[0].Id,
                    UploadedByUserId = users[1].Id,
                    UploadedBy = users[1]
                },
                new Document
                {
                    FileName = "johnson_inspection_cert.pdf",
                    FilePath = "/uploads/installations/1/inspection.pdf",
                    FileType = "pdf",
                    FileSize = 819200,
                    Type = DocumentType.InspectionCertificate,
                    UploadedAt = DateTime.UtcNow.AddMonths(-1),
                    Description = "Final inspection certificate from city",
                    InstallationId = installations[0].Id,
                    UploadedByUserId = users[0].Id,
                    UploadedBy = users[0]
                },
                // Ticket documents
                new Document
                {
                    FileName = "inverter_error_screenshot.jpg",
                    FilePath = "/uploads/tickets/1/error_screen.jpg",
                    FileType = "jpg",
                    FileSize = 1572864,
                    Type = DocumentType.ProblemPhoto,
                    UploadedAt = DateTime.UtcNow.AddDays(-3),
                    Description = "Photo of inverter error display",
                    TicketId = supportTickets[0].Id,
                    UploadedByUserId = users[0].Id,
                    UploadedBy = users[0]
                }
            };
            await context.Documents.AddRangeAsync(documents);
            await context.SaveChangesAsync();

            Console.WriteLine("Database seeding completed successfully!");
            Console.WriteLine($"Created:");
            Console.WriteLine($"  - {addresses.Count} addresses");
            Console.WriteLine($"  - {users.Count} users");
            Console.WriteLine($"  - {customers.Count} customers");
            Console.WriteLine($"  - {installations.Count} installations");
            Console.WriteLine($"  - {statusHistories.Count} status history records");
            Console.WriteLine($"  - {technicianAssignments.Count} technician assignments");
            Console.WriteLine($"  - {weatherData.Count} weather records");
            Console.WriteLine($"  - {energyProductions.Count} energy production records");
            Console.WriteLine($"  - {equipment.Count} equipment items");
            Console.WriteLine($"  - {supportTickets.Count} support tickets");
            Console.WriteLine($"  - {documents.Count} documents");
        }
    }
}