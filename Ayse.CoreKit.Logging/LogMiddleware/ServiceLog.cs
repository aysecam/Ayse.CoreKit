
namespace Ayse.CoreKit.Logging.LogMiddleware;

public class ServiceLog
{
    public int Id { get; set; }

    // Hangi servis/metot çağrıldı? (örnek: /api/users/login)
    public string? Path { get; set; }

    // HTTP metodu (GET, POST, vs.)
    public string? Method { get; set; }

    // Gelen/giden veri (body)
    public string? Body { get; set; }

    // Query string varsa onu da kaydedelim
    public string? QueryString { get; set; }

    // Status kodu sadece response için dolacaktır
    public int? StatusCode { get; set; }

    // Log tipi (true = request, false = response)
    public bool IsRequest { get; set; }

    // Kullanıcı kimliği varsa
    public string? UserIdentifier { get; set; }

    // Request-response eşleştirme için
    public Guid TransactionId { get; set; }

    // Log zamanı
    public DateTime CreatedDate { get; set; }
}
