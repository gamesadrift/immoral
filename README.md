# (IM)MORAL

![Logo Games Adrift](/Images_Web/GALogo.png)

## (ES) Español
Esta es la página en español. ¡Pero tenemos otra en [inglés](https://gamesadrift.github.io/immoral/README_EN)!

### Introducción

> **(IM)MORAL** es un juego **competitivo**, que puede parecer **rápido** y **caótico** al principio pero en el que, con **estrategia**, se puede ganar cualquier partida.
>
> La temática del juego es la conservación de espacios, ya sean naturales o urbanos, en definitiva, ser un buen ciudadano y **cuidar del medio ambiente**. Para esto los jugadores pueden utilizar a **Bernie**, el buenazo y altruista héroe de esta historia que busca mantener todo en orden; o a **Jonas** que le persigue destruyendo todo lo que toca.
>
> En este repositorio se encuentran el código utilizado en nuestro juego (o puedes descargartelo de nuestro [**itch.io**](https://gamesadrift.itch.io/immoral)).

### Objetivo

> El objetivo es sencillo, ambos jugadores tienen 3 minutos para interactuar con el escenario que haya tocado (actualmente bosque o ciudad).
> Interactuar con los elementos da puntos (según el elemento).
> Al acabarse el tiempo el que tenga más puntos gana.

### Controles

> #### Movimiento
> Las teclas **W**, **A**, **S** y **D** nos mueven en el escenario hacia delante, izquierda, detrás y derecha respectivamente.

> #### Interacción
> La tecla **barra espaciadora** sirve, cuando estamos pegados a un objeto interactuable, para ganar puntos del mismo (ver elementos).

> #### Salir
> Al pulsar **escape**, en partida, aparece un menú que te permite volver a la pantalla principal.

![Teclas WASD](/Images_Web/wasd.png)

### Objetos

> #### Elementos
> - **Asimétrico**: un jugador puede activarlo para ganar puntos al pasar el tiempo, pero si está activado el rival puede destruirlo para ganar puntos.
> - **Progresivo**: si un jugador lo activa empieza a subir de nivel y generar puntos (de acuerdo al nivel), pero si el otro interactua con el vuelve al primer nivel y empieza a dar puntos a este. Si llega al máximo nivel da bastantes puntos y desaparece.
> - ***OneTime***: del inglés "una vez" pues solo se puede activar una vez, el que lo active primero se llevará puntos.
> - **Reversible**: empieza de un bando, si lo activa el otro gana puntos pero cambia de bando, cada vez da menos hasta que desaparece.

> #### Ejemplos (Bosque)
> - **Asimétrico**: la hoguera la activa Jonas, pero Bernie puede destruirla si está encendida.
> - **Progresivo**: el árbol genera cada vez más fruta si lo activa Bernie o se quema cada vez más si lo activa Jonas.
> - ***OneTime***: las flores pueden ser regadas por Bernie o quemadas por Jonas.
> - **Reversible**: la basura puede estar recogida (dentro del contenedor) si lo ha tocado Bernie o esparcida si lo ha tocado Jonas.

> #### Pociones
> Actualmente hay dos: **amarilla** para correr y **azul** para ganar el doble de puntos durante un tiempo.

![Banco(Reversible)](/Images_Web/RRSS_Bench.jpg)

### Herramientas y demás

> El juego está desarrollado puramente en **Unity** y **C#**. Lo que se encuentra aquí es solo el código del mismo, hay partes que requieren del conocimiento de cómo trabaja Unity o las posibilidades que da a la hora de asociar elementos desde el editor, etcétera. Si es por aprender estamos abiertos a preguntas, dentro de nuestra capacidad.
>
> El juego es **multijugador** y para ello se ha utilizado **PUN** (Photon Unity Network, de Photon 2). También puedes hacernos preguntas respecto a esto.

![Unity, C# y Photon (Cloud) Logo](/Images_Web/logos.png)

### Código

> Como dijimos antes, el código se encuentra en nuestro repositorio de GitHub (el botón de arriba del todo debería llevarte ;D). El código está comentado en español pero para los nombres de variables o funciones hemos utilizado inglés. Si esto es un impedimento puedes intentar utilizar un traductor o contactarnos con alguno de los enlaces del siguiente apartado.

### Contacto

> #### Itch.io
> [gamesadrift](https://gamesadrift.itch.io/immoral)
> #### Twitter
> [@GamesAdrift](https://twitter.com/GamesAdrift)
> #### Instagram
> [@gamesadrift](https://www.instagram.com/gamesadrift/)
> #### YouTube
> [Games Adrift](https://www.youtube.com/channel/UCRG2y9zJj4lvZebusqPuxQA)
> #### Gmail
> games.adrift@gmail.com
