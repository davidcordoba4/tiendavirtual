Tienda Virtual Prueba EVERTEC

ng version:

Angular CLI: 6.1.1
Node: 12.11.0

Pasos Instalación URL Rewrite Module en IIS 10
1. Abra Regedit ruta HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\InetStp
2.Edite "MajorVersion" y establezca el valor DECIMAL en 9
3. Presiona F5 mientras estás en Regedit
4. Ahora instale el módulo ReWrite 2.0.
5. Cambie "MajorVersion" de nuevo a "DECIMAL" valor de 10
6. Presione F5 mientras está en Regedit
7. Cerrar Regedit

Cadena Conexión BD Sql Backend:
Web Config --> connectionStrings --> BDTiendaVirtual

Parámetros configurables conexión servicio Place to Pay:
Web Config --> appSettings

Web config publicado front:
Carpeta Web config front

Configurar Url backend environment front para publicado:
environments --> environment.devremote.ts --> Valor campo urlHost

Configurar Url backend environment local para ejecutar ng serve o npm start:
environments --> environment.ts --> Valor campo urlHost

Generar publicado front:
npm run build-devremote

Generar publicado Backend:
Publicar proyecto WebApiTiendaVirtual con la solución abierta en Visual Studio en la carpeta que alojará el sitio del backend en IIS

Antes de ejecutar front local con ng serve o generar publicado ejecutar desde linea de comandos en ruta UI\Web:
npm i

Version Visual Studio Solución Backend:
Visual Studio Community 2017

Backup BD Sql:
TiendaVirtual.bak

Para correr backend localmente colocar como proyecto de inicio WebApiTiendaVirtual

Las pruebas unitarias se encuentran en el proyecto UnitTestTiendaVirtual, en caso de que no ejecuten de forma correcta intentar recompilar toda la solución.