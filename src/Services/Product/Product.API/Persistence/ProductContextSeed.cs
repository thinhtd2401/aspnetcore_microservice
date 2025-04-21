namespace Product.API.Persistence;

public class ProductContextSeed
{
    public static async Task SeedAsync(ProductContext productContext, ILogger logger)
    {
        if (!productContext.Products.Any())
        {
            productContext.AddRange(GetPreconfiguredProducts());
            await productContext.SaveChangesAsync();
            logger.LogInformation("Seeded database with products.");
        }
        else
        {
            logger.LogInformation("Database already seeded with products.");
        }
    }
    private static IEnumerable<Entities.Product> GetPreconfiguredProducts()
    {
        var existingNos = new HashSet<string>();
        return new List<Entities.Product>
        {
            new()
            {
                Id = 1,
                No = GenerateUniqueNo(existingNos),
                Name = "iPhone 15 Pro Max",
                Summary = "Titanium design, A17 Pro chip, 5x optical zoom",
                Description =
                    "iPhone 15 Pro Max with a strong titanium build, the powerful A17 Pro chip, and advanced camera system with 5x optical zoom.",
                Price = (decimal)1199.00
            },
            new()
            {
                Id = 2,
                No = GenerateUniqueNo(existingNos),
                Name = "iPhone 15",
                Summary = "Dynamic Island, A16 Bionic chip, advanced dual-camera",
                Description =
                    "iPhone 15 featuring Dynamic Island, powered by the A16 Bionic chip, and a new advanced dual-camera system.",
                Price = (decimal)799.00
            },
            new()
            {
                Id = 3,
                No = GenerateUniqueNo(existingNos),
                Name = "iPhone 15 Plus",
                Summary = "Bigger screen, Dynamic Island, powerful battery life",
                Description =
                    "iPhone 15 Plus with a larger display, Dynamic Island feature, and industry-leading battery performance.",
                Price = (decimal)899.00
            },
            new()
            {
                Id = 4,
                No = GenerateUniqueNo(existingNos),
                Name = "iPhone 15 Pro",
                Summary = "Lightweight titanium, A17 Pro chip, pro-level cameras",
                Description =
                    "iPhone 15 Pro made with lightweight titanium, powered by the A17 Pro chip, featuring pro-grade photography tools.",
                Price = (decimal)999.00
            },
            new()
            {   
                Id = 5,
                No = GenerateUniqueNo(existingNos),
                Name = "iPhone SE (2024)",
                Summary = "Affordable power, A15 Bionic chip, compact design",
                Description =
                    "iPhone SE 2024 with an A15 Bionic chip, classic compact design, and excellent performance at a great value.",
                Price = (decimal)429.00,
            },
            new()
            {
                Id = 6,
                No = GenerateUniqueNo(existingNos),
                Name = "MacBook Pro 14\" (M3)",
                Summary = "M3 chip, Liquid Retina XDR display, all-day battery",
                Description =
                    "MacBook Pro 14-inch model powered by the M3 chip, featuring a stunning Liquid Retina XDR display and long battery life.",
                Price = (decimal)1999.00
            },
            new()
            {
                Id = 7,
                No = GenerateUniqueNo(existingNos),
                Name = "MacBook Pro 16\" (M3 Max)",
                Summary = "Ultimate power, M3 Max chip, expansive screen",
                Description =
                    "MacBook Pro 16-inch model with M3 Max chip, designed for extreme performance and a breathtaking expansive display.",
                Price = (decimal)3499.00
            },
            new()
            {
                Id = 8,
                No = GenerateUniqueNo(existingNos),
                Name = "MacBook Air 13\" (M3)",
                Summary = "Ultra-portable, M3 chip, incredible battery life",
                Description =
                    "MacBook Air 13-inch model with M3 chip, offering exceptional portability and outstanding all-day battery performance.",
                Price = (decimal)1099.00
            },
            new()
            {
                Id = 9,
                No = GenerateUniqueNo(existingNos),
                Name = "MacBook Air 15\" (M3)",
                Summary = "Bigger Air, M3 chip, thin and light design",
                Description =
                    "MacBook Air 15-inch model powered by the M3 chip, combining a larger screen with a remarkably thin and light design.",
                Price = (decimal)1299.00
            },
            new()
            {
                Id = 10,
                No = GenerateUniqueNo(existingNos),
                Name = "MacBook Pro 14\" (M3 Pro)",
                Summary = "Professional-grade, M3 Pro chip, enhanced graphics",
                Description =
                    "MacBook Pro 14-inch model equipped with the M3 Pro chip, offering professional-grade performance and enhanced graphics capabilities.",
                Price = (decimal)2499.00
            }
        };
    }
    
    private static string GenerateUniqueNo(HashSet<string> existingNos)
    {
        var random = new Random();
        string no;

        do
        {
            no = $"P{random.Next(100000, 999999)}"; // Example: P123456
        } while (existingNos.Contains(no));

        existingNos.Add(no);
        return no;
    }
}