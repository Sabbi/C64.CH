function openNav() {
    document.getElementById("mobileNav").style.width = "100%";
    $("body").addClass("no-scroll");
}

function closeNav() {
    document.getElementById("mobileNav").style.width = "0%";
    $("body").removeClass("no-scroll");
}

$(".nav-items").slideUp();

function onTrigger(element) {
    var current = $(element).next(".nav-items");
    $(".nav-items").not(current).slideUp();
    current.slideToggle();
}

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
function submitForm(id) {
    console.log("Subtmit " + id);
    document.getElementById(id).submit();
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

function ResetGroupEditSelect() {
    groupEditSelect.selectedIndex = 0
}

window.scrollToElementId = (elementId) => {
    console.info('scrolling to element', elementId);
    var element = document.getElementById(elementId);
    if (!element) {
        console.warn('element was not found', elementId);
        return false;
    }
    element.scrollIntoView();
    return true;
}

/* VICE.JS HACKS */
var inited = false;
var emulator;
var isStarted = false;
function setupEmu(params, sidModel = 0, driveSoundEmulation = 0, driveSoundEmulationVolume = 0, crtEmulation = 0) {
    var diskimages = params[0];
    var fliplist = params[1];

    //console.log(diskimages);
    //console.log(fliplist);

    var mountFiles = new Array();

    for (var i = 0; i < diskimages.length; i++) {
        console.log(i + " - Diskimage: " + diskimages[i]);

        mountFiles[i] = VICELoader.mountFile("disk" + (i + 1) + ".d64", VICELoader.fetchFile("disk" + (i + 1) + ".d64", "/data/productions/archive/" + diskimages[i]));
    }
    var autoLoad = VICELoader.autoLoad("disk1.d64");

    var finalFlipList = new Array();
    for (var i = 0; i < fliplist.length; i++) {
        finalFlipList[i] = "disk" + fliplist[i] + ".d64";
    }

    //console.log(finalFlipList);

    var viceLoader = new VICELoader(VICELoader.emulatorJS("/js/x64-2.min.js"),
        VICELoader.nativeResolution(0, 0),
        VICELoader.extraArgs([
            "-soundfragsize", "4",
            "-soundrate", "48000",
            "-soundsync", "2",
            "-soundbufsize", "150",
            "-residsamp", "0",
            "-config", "/emulator/vice.ini"]),

        VICELoader.mountFile("vice.ini",
            VICELoader.fetchFile("Configuration",
                "/data/vice.ini?sidModel=" + sidModel + "&driveSoundEmulation=" + driveSoundEmulation + "&driveSoundEmulationVolume=" + driveSoundEmulationVolume + "&crtEmulation=" + crtEmulation)),
        mountFiles[0],
        mountFiles[1],
        mountFiles[2],
        mountFiles[3],
        mountFiles[4],
        VICELoader.fliplist([finalFlipList]),
        //   VICELoader.autoLoad("eod-1.d64"),
        autoLoad
    );

    emulator = new Emulator(document.querySelector("#canvas"), null, viceLoader);
}

function startEmu() {
    emulator.start({ waitAfterDownloading: false });
    $("#canvas").css("display", "block");
    isStarted = true;
}

function exitEmu() {
    try {
        Module.exit(1);
    }
    catch (e) {
    }
    isStarted = false;

    var keyboardListeningElement = Module["keyboardListeningElement"] || document;
    keyboardListeningElement.removeEventListener("keydown", SDL.receiveEvent);
    keyboardListeningElement.removeEventListener("keyup", SDL.receiveEvent);
    keyboardListeningElement.removeEventListener("keypress", SDL.receiveEvent);
    window.removeEventListener("focus", SDL.receiveEvent);
    window.removeEventListener("blur", SDL.receiveEvent);
    document.removeEventListener("visibilitychange", SDL.receiveEvent);
    window.removeEventListener("unload", SDL.receiveEvent);

    // Start fix https://github.com/Sabbi/C64.CH/issues/119
    function keypress(e) {
        return true;
    }
    window.onkeydown = keypress;
    // End fix
}

function goFullScreen() {
    try {
        if (isStarted)
            emulator.requestFullScreen();
    }
    catch (e) { }
}

cleanDb();

function cleanDb() {
    try {
        console.log("CleanDB");
        var DBOpenRequest = window.indexedDB.open("emularity", 4);

        DBOpenRequest.onsuccess = function (event) {
            console.log('<li>Database initialised.</li>');

            // store the result of opening the database in the db variable.
            // This is used a lot below
            var db = DBOpenRequest.result;

            // Clear all the data form the object store
            clearData(db);
        };
    }
    catch (e) {
    }
}

function clearData(db) {
    try {
        // open a read/write db transaction, ready for clearing the data
        var transaction = db.transaction(["emularity"], "readwrite");

        // report on the success of the transaction completing, when everything is done
        transaction.oncomplete = function (event) {
            console.log('<li>Transaction completed.</li>');
        };

        transaction.onerror = function (event) {
            console.log('<li>Transaction not opened due to error: ' + transaction.error + '</li>');
        };

        // create an object store on the transaction
        var objectStore = transaction.objectStore("emularity");

        // Make a request to clear all the data out of the object store
        var objectStoreRequest = objectStore.clear();

        objectStoreRequest.onsuccess = function (event) {
            // report the success of our request
            console.log('<li>Request successful.</li>');
        };
    }
    catch (e) { }
};

function grabEmuScreenshot() {
    var dataUrl = canvas.toDataURL("image/png");
    return dataUrl;
}