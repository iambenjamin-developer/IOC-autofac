UPDATE Usuarios
SET IdRol = @IdRol, Usuario = @Usuario, Nombre = @Nombre,
Apellido = @Apellido, Password = @Password, PasswordSalt = @PasswordSalt,
FechaCreacion = @FechaCreacion, Activo = @Activo
WHERE Id = @Id