using System;
using System.Collections.Generic;

namespace edok_kiosk.Model;

public partial class Moderator
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public byte[]? Pfp { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }
}
