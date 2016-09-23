var working = false;
$('.login').on('submit', function (e) {
    var url = $(location).attr('href');      // http://www.refulz.com:8082/index.php#tab2
    var page = $(location).attr('pathname');  // index.php
    url = url.replace(page, "login.aspx");
    e.preventDefault();
    if (working) return;
    working = true;
    var $this = $(this),
      $state = $this.find('button > .state');
    $this.addClass('loading');
    $state.html('Validando Usuario');
    setTimeout(function () {
        $this.addClass('ok');
        $state.html('Bienvenido');
        setTimeout(function () {
            window.location.replace(url);
            $state.html('Iniciar Sesión');
            $this.removeClass('ok loading');
            working = false;
        }, 1000);
    }, 3000);
});

function Carga() {
    var $this = $(btn),
        $state = $this.find('button > .state');
    $this.addClass('loading');
    $state.html('Validando Usuario');
    setTimeout(function () {
        $this.addClass('ok');
        $state.html('Bienvenido');
        setTimeout(function () {
            window.location.replace(url);
            $state.html('Iniciar Sesión');
            $this.removeClass('ok loading');
            working = false;
        }, 4000);
    }, 3000);
}