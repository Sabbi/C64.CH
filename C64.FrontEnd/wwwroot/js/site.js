function openNav() {
    document.getElementById("mobileNav").style.width = "100%";
    $("body").addClass("no-scroll");
}

function closeNav() {
    document.getElementById("mobileNav").style.width = "0%";
    $("body").removeClass("no-scroll");
}

$(".nav-items").slideUp();
$(".trigger").click(function () {
    var current = $(this).next(".nav-items");
    $(".nav-items").not(current).slideUp();
    current.slideToggle();
});

toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-top-full-width",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "3000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

window.initializeCarousel = () => {
    console.log("Initialize carousel");

    $('#carousel1').carousel({ interval: 20000 });
    $('#carousel1').carousel(0);
    $('#carousel1-prev').click(
        () => $('#carousel1').carousel('prev'));
    $('#carousel1-next').click(
        () => $('#carousel1').carousel('next'));
}

window.bindCarousel = () => {
    $('#carousel1').on('slide.bs.carousel', function (e) {
        $(e.relatedTarget).find('img').each(function () {
            var $this = $(this);
            var src = $this.data('lazy-load-src');

            if (typeof src !== "undefined" && src != "") {
                $this.attr('src', src)
                $this.data('lazy-load-src', '');
            }
        });
    });

    $('#carousel1').bind('slide.bs.carousel', function (e) {
        $("[data-slide-to='" + e.from + "'").removeClass("active");
        $("[data-slide-to='" + e.to + "'").addClass("active");
    });
}

function submitLogin() {
    document.getElementById('headerLoginForm').submit();
}

function saveAsFile(filename, bytesBase64) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
}

function initOwlCarousel() {
    var owl = $(".owl-carousel");
    owl.owlCarousel({
        responsive: {
            0: {
                items: 2
            },
            576: {
                items: 3
            },
            768: {
                items: 4
            },
        },
        loop: false,
        margin: 10,
        autoplay: true,
        autoplayTimeout: 10000,
        autoplayHoverPause: true
    });
}

function initSelectPicker() {
    $('.selectpicker').selectpicker();
}

function ResetProdEditSelect() {
    prodEditSelect.selectedIndex = 0
}