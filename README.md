# TestCampus
Test Campus

FrontEnd

Desarrollado en angular, usando angular material, ngx-mat-select-search y ngx-captcha

![image](https://user-images.githubusercontent.com/21958802/212497768-59c156ef-32a3-4a4c-b251-48b4d2fd9805.png)

Backend

Se desarrollo en .Net 6, se crearon dos servicios una para la obtencion de las ciudades y otro para el envio de email.

![image](https://user-images.githubusercontent.com/21958802/212497812-0e5c0915-3ef9-409c-97a6-987e7999bf39.png)

Para obtener las ciudades se uso la API https://andruxnet-world-cities-v1.p.rapidapi.com, cada que se escribe un texto mayor a 3 caracteres el servicio va y consulta en el servicio y trae las ciudades correspondientes (Filter Server Side).

Para el envio de correo se creo un template en html para que se envie de una manera estetica el email:
![image](https://user-images.githubusercontent.com/21958802/212497872-5529607f-d990-49c4-a3ea-60625524248b.png)

Se uso la libreria System.Net.Mail para el envio del email, se configuro el envio desde un correo de Microsoft

![image](https://user-images.githubusercontent.com/21958802/212497944-b5585db2-d96c-43f3-80d4-4514235e4712.png)

