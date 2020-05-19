 SELECT USUARIOS.Id AS ID_USUARIO, Roles.Descripcion AS ROL,
            USUARIOS.Usuario AS USERNAME, USUARIOS.Nombre AS NOMBRES, USUARIOS.Apellido AS APELLIDOS,
            USUARIOS.FechaCreacion AS FECHA_CREACION, USUARIOS.Activo AS ACTIVO,
            CASE  
             WHEN Activo = 1 THEN 'ACTIVO'   
             ELSE 'BAJA'  
             END  AS ESTADO
            FROM USUARIOS
            INNER JOIN ROLES ON
            Usuarios.IdRol = Roles.Id