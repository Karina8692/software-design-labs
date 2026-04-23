using Microsoft.EntityFrameworkCore;
using MessengerApp.Models;

namespace MessengerApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> Users => Set<User>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<MessageAudit> AuditLogs => Set<MessageAudit>();
}
