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

function initializeCarousel() {
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
    if (typeof (prodEditSelect) != "undefined")
        prodEditSelect.selectedIndex = 0
}

function ResetGroupEditSelect() {
    if (typeof (groupEditSelect) != "undefined")
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
    var overrideStar = params[2];

    //console.log(diskimages);
    //console.log(fliplist);

    var mountFiles = new Array();

    for (var i = 0; i < diskimages.length; i++) {
        //      console.log(i + " - Diskimage: " + diskimages[i]);
        mountFiles[i] = VICELoader.mountFile("disk" + (i + 1) + ".d64", VICELoader.fetchFile("disk" + (i + 1) + ".d64", "/data/productions/archive/" + diskimages[i]));
    }

    var autoLoad;

    if (overrideStar)
        autoLoad = VICELoader.autoLoad("disk1.d64:" + overrideStar);
    else
        autoLoad = VICELoader.autoLoad("disk1.d64");

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

var wasFullscreen = false;

window.onload = window.onresize = function () {
    if (typeof canvas !== 'undefined')
        if (wasFullscreen) {
            wasFullscreen = false;
            resize(768, 544);
        }
    setTimeout(() => {
        if (window.innerHeight == 1080 || window.innerHeight == 1440 || window.innerHeight == 1050 || window.innerHeight == 2160) {
            wasFullscreen = true;
            resize(768, window.innerHeight == 1050 ? 525 : 540);
        }
    }, 25);
}

function goFullScreen() {
    try {
        if (isStarted) {
            emulator.requestFullScreen();
        }
    }
    catch (e) { }
}

function resize(w, h) {
    // create a temporary canvas obj to cache the pixel data //
    var temp_cnvs = document.createElement('canvas');
    var temp_cntx = temp_cnvs.getContext('2d');
    // set it to the new width & height and draw the current canvas data into it //
    temp_cnvs.width = w;
    temp_cnvs.height = h;
    temp_cntx.drawImage(canvas, 0, 0);
    // resize & clear the original canvas and copy back in the cached pixel data //
    canvas.width = w;
    canvas.height = h;

    var _context = canvas.getContext("2d");
    _context.drawImage(temp_cnvs, 0, 0);
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

function insertNextDisk() {
    document.dispatchEvent(
        new KeyboardEvent("keydown", {
            key: "n",
            altKey: true
        })
    );
    document.dispatchEvent(
        new KeyboardEvent("keyup", {
            key: "n",
            altKey: true
        })
    );
}

function stateToTrue() {
    $(".emulator-control").removeClass("invisible");
}

function stateToFalse() {
    $(".emulator-control").addClass("invisible");
}

function BlazorScrollToId(id) {
    const element = document.getElementById(id);
    if (element instanceof HTMLElement) {
        element.scrollIntoView({
            behavior: "auto",
            block: "start",
            inline: "nearest"
        });
    }
}

function isFirefox() {
    return navigator.userAgent.toLowerCase().indexOf('firefox') > -1;
}

// https://stackoverflow.com/questions/13735912/anchor-jumping-by-using-javascript
function jump(h) {
    var url = location.href;
    location.href = "#" + h;
    history.replaceState(null, null, url);
}

function createCookieBsCookie() {
    var date = new Date();
    date.setTime(date.getTime() + (1000000 * 24 * 60 * 60 * 1000));
    var expires = "; expires=" + date.toGMTString();
    document.cookie = "CookieBs=BsOk" + expires + "; path=/";
}

function readBsCookie() {
    var cookies = document.cookie;

    if (cookies) {
        if (cookies.includes("CookieBs")) {
            return true;
        }
    }

    return false;
}