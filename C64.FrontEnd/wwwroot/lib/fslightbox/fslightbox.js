! function (e, t) {
    if ("object" == typeof exports && "object" == typeof module) module.exports = t();
    else if ("function" == typeof define && define.amd) define([], t);
    else {
        var n = t();
        for (var o in n) ("object" == typeof exports ? exports : e)[o] = n[o]
    }
}(window, (function () {
    return function (e) {
        var t = {};

        function n(o) {
            if (t[o]) return t[o].exports;
            var s = t[o] = {
                i: o,
                l: !1,
                exports: {}
            };
            return e[o].call(s.exports, s, s.exports, n), s.l = !0, s.exports
        }
        return n.m = e, n.c = t, n.d = function (e, t, o) {
            n.o(e, t) || Object.defineProperty(e, t, {
                enumerable: !0,
                get: o
            })
        }, n.r = function (e) {
            "undefined" != typeof Symbol && Symbol.toStringTag && Object.defineProperty(e, Symbol.toStringTag, {
                value: "Module"
            }), Object.defineProperty(e, "__esModule", {
                value: !0
            })
        }, n.t = function (e, t) {
            if (1 & t && (e = n(e)), 8 & t) return e;
            if (4 & t && "object" == typeof e && e && e.__esModule) return e;
            var o = Object.create(null);
            if (n.r(o), Object.defineProperty(o, "default", {
                enumerable: !0,
                value: e
            }), 2 & t && "string" != typeof e)
                for (var s in e) n.d(o, s, function (t) {
                    return e[t]
                }.bind(null, s));
            return o
        }, n.n = function (e) {
            var t = e && e.__esModule ? function () {
                return e.default
            } : function () {
                return e
            };
            return n.d(t, "a", t), t
        }, n.o = function (e, t) {
            return Object.prototype.hasOwnProperty.call(e, t)
        }, n.p = "", n(n.s = 0)
    }([function (e, t, n) {
        "use strict";
        n.r(t);
        var o, s = "fslightbox-",
            i = "".concat(s, "styles"),
            r = "".concat(s, "cursor-grabbing"),
            c = "".concat(s, "full-dimension"),
            a = "".concat(s, "flex-centered"),
            l = "".concat(s, "open"),
            u = "".concat(s, "transform-transition"),
            d = "".concat(s, "absoluted"),
            p = "".concat(s, "slide-btn"),
            f = "".concat(p, "-container"),
            h = "".concat(s, "fade-in"),
            g = "".concat(s, "fade-out"),
            m = h + "-strong",
            v = g + "-strong",
            b = "".concat(s, "opacity-"),
            x = "".concat(b, "1"),
            y = "".concat(s, "source"),
            S = "".concat(y, "-outer");

        function w(e) {
            return (w = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function (e) {
                return typeof e
            } : function (e) {
                return e && "function" == typeof Symbol && e.constructor === Symbol && e !== Symbol.prototype ? "symbol" : typeof e
            })(e)
        }
        "object" === ("undefined" == typeof document ? "undefined" : w(document)) && ((o = document.createElement("style")).className = i, o.appendChild(document.createTextNode(".fslightbox-absoluted{position:absolute;top:0;left:0}.fslightbox-fade-in{animation:fslightbox-fade-in .25s cubic-bezier(0,0,.7,1)}.fslightbox-fade-out{animation:fslightbox-fade-out .25s ease}.fslightbox-fade-in-strong{}.fslightbox-fade-out-strong{animation:fslightbox-fade-out-strong .25s ease}@keyframes fslightbox-fade-in{from{opacity:.65}to{opacity:1}}@keyframes fslightbox-fade-out{from{opacity:.35}to{opacity:0}}@keyframes fslightbox-fade-in-strong{from{opacity:.3}to{opacity:1}}@keyframes fslightbox-fade-out-strong{from{opacity:1}to{opacity:0}}.fslightbox-cursor-grabbing{cursor:grabbing}.fslightbox-full-dimension{width:100%;height:100%}.fslightbox-open{overflow:hidden;height:100%}.fslightbox-flex-centered{display:flex;justify-content:center;align-items:center}.fslightbox-opacity-0{opacity:0!important}.fslightbox-opacity-1{opacity:1!important}.fslightbox-scrollbarfix{padding-right:17px}.fslightbox-transform-transition{transition:transform .3s}.fslightbox-container{font-family:Helvetica,sans-serif;position:fixed;top:0;left:0;background:linear-gradient(rgba(30,30,30,.9),#000 1810%);z-index:9999999;-webkit-user-select:none;-moz-user-select:none;-ms-user-select:none;user-select:none;-webkit-tap-highlight-color:transparent}.fslightbox-container *{box-sizing:border-box}.fslightbox-svg-path{transition:fill .15s ease;fill:#ddd}.fslightbox-nav{height:45px;width:100%;position:absolute;top:0;left:0}.fslightbox-slide-number-container{display:flex;justify-content:center;align-items:center;position:relative;height:100%;font-size:15px;color:#d7d7d7;z-index:0;max-width:55px;text-align:left}.fslightbox-slide-number-container .fslightbox-flex-centered{height:100%}.fslightbox-slash{display:block;margin:0 5px;width:1px;height:12px;transform:rotate(15deg);background:#fff}.fslightbox-toolbar{position:absolute;z-index:3;right:0;top:0;height:100%;display:flex;background:rgba(35,35,35,.65)}.fslightbox-toolbar-button{height:100%;width:45px;cursor:pointer}.fslightbox-toolbar-button:hover .fslightbox-svg-path{fill:#fff}.fslightbox-slide-btn-container{display:flex;align-items:center;padding:12px 12px 12px 6px;position:absolute;top:50%;cursor:pointer;z-index:3;transform:translateY(-50%)}@media (min-width:476px){.fslightbox-slide-btn-container{padding:22px 22px 22px 6px}}@media (min-width:768px){.fslightbox-slide-btn-container{padding:30px 30px 30px 6px}}.fslightbox-slide-btn-container:hover .fslightbox-svg-path{fill:#f1f1f1}.fslightbox-slide-btn{padding:9px;font-size:26px;background:rgba(35,35,35,.65)}@media (min-width:768px){.fslightbox-slide-btn{padding:10px}}@media (min-width:1600px){.fslightbox-slide-btn{padding:11px}}.fslightbox-slide-btn-container-previous{left:0}@media (max-width:475.99px){.fslightbox-slide-btn-container-previous{padding-left:3px}}.fslightbox-slide-btn-container-next{right:0;padding-left:12px;padding-right:3px}@media (min-width:476px){.fslightbox-slide-btn-container-next{padding-left:22px}}@media (min-width:768px){.fslightbox-slide-btn-container-next{padding-left:30px}}@media (min-width:476px){.fslightbox-slide-btn-container-next{padding-right:6px}}.fslightbox-down-event-detector{position:absolute;z-index:1}.fslightbox-slide-swiping-hoverer{z-index:4}.fslightbox-invalid-file-wrapper{font-size:22px;color:#eaebeb;margin:auto}.fslightbox-video{object-fit:cover}.fslightbox-youtube-iframe{border:0}.fslightbox-loader{display:block;margin:auto;position:absolute;top:50%;left:50%;transform:translate(-50%,-50%);width:67px;height:67px}.fslightbox-loader div{box-sizing:border-box;display:block;position:absolute;width:54px;height:54px;margin:6px;border:5px solid;border-color:#999 transparent transparent transparent;border-radius:50%;animation:fslightbox-loader 1.2s cubic-bezier(.5,0,.5,1) infinite}.fslightbox-loader div:nth-child(1){animation-delay:-.45s}.fslightbox-loader div:nth-child(2){animation-delay:-.3s}.fslightbox-loader div:nth-child(3){animation-delay:-.15s}@keyframes fslightbox-loader{0%{transform:rotate(0)}100%{transform:rotate(360deg)}}.fslightbox-source{position:relative;margin:auto;opacity:0;z-index:2;backface-visibility:hidden;transform:translateZ(0);transition:opacity .3s;will-change:opacity}.fslightbox-source-outer{will-change:transform}")), document.head.appendChild(o));

        function L(e) {
            var t, n = e.props,
                o = 0,
                s = {};
            this.getSourceTypeFromLocalStorageByUrl = function (e) {
                return t[e] ? t[e] : i(e)
            }, this.handleReceivedSourceTypeForUrl = function (e, n) {
                void 0 !== s[n] && (o--, s[n] = e, 0 === o && (! function (e, t) {
                    for (var n in t) e[n] = t[n]
                }(t, s), localStorage.setItem("fslightbox-types", JSON.stringify(t))))
            };
            var i = function (e) {
                o++, s[e] = !1
            };
            n.disableLocalStorage ? (this.getSourceTypeFromLocalStorageByUrl = function () { }, this.handleReceivedSourceTypeForUrl = function () { }) : (t = JSON.parse(localStorage.getItem("fslightbox-types"))) || (t = {}, this.getSourceTypeFromLocalStorageByUrl = i)
        }

        function F(e, t, n, o) {
            var s = e.data,
                i = e.elements.sources,
                r = n / o,
                c = 0;

            // Why? No Idea.. but it does the job (so far)
            n = 2000;
            o = 2000 * r;

            this.styleSize = function () {
                if ((c = s.maxSourceWidth / r) < s.maxSourceHeight) return n < s.maxSourceWidth && (c = o), a();
                c = o > s.maxSourceHeight ? s.maxSourceHeight : o, a()
            };
            var a = function () {
                i[t].style.width = c * r + "px", i[t].style.height = c + "px"
            }
        }

        function C(e, t, n, o) {
            var s = this,
                i = e.collections.sourcesStylers,
                r = e.elements,
                c = r.sources,
                a = r.sourcesInners,
                l = r.sourcesOuters,
                u = e.resolve;
            this.runNormalLoadActions = function () {
                c[t].classList.add(x), a[t].classList.add(m), l[t].removeChild(l[t].firstChild)
            }, this.runInitialLoadActions = function () {
                s.runNormalLoadActions();
                var e = u(F, [t, n, o]);
                e.styleSize(), i[t] = e
            }
        }

        function I(e, t) {
            var n, o = this,
                s = e.elements.sources,
                i = e.props,
                r = e.resolve;
            this.handleImageLoad = function (e) {
                var t = e.target,
                    n = t.width,
                    s = t.height;
                o.handleImageLoad = c(n, s)
            }, this.handleVideoLoad = function (e) {
                var t = e.target,
                    s = t.videoWidth,
                    i = t.videoHeight;
                n = !0, o.handleVideoLoad = c(s, i)
            }, this.handleNotMetaDatedVideoLoad = function () {
                n || o.handleYoutubeLoad()
            }, this.handleYoutubeLoad = function () {
                var e = 1920,
                    t = 1080;
                i.maxYoutubeDimensions && (e = i.maxYoutubeDimensions.width, t = i.maxYoutubeDimensions.height), o.handleYoutubeLoad = c(e, t)
            }, this.handleCustomLoad = function () {
                setTimeout((function () {
                    o.handleCustomLoad = c(s[t].offsetWidth, s[t].offsetHeight)
                }))
            };
            var c = function (e, n) {
                var o = r(C, [t, e, n]);
                return o.runInitialLoadActions(), o.runNormalLoadActions
            }
        }

        function E(e, t) {
            var n = e.collections.sourcesLoadsHandlers,
                o = e.elements,
                s = o.sources,
                i = o.sourcesInners,
                r = e.props.sources;
            s[t] = document.createElement("img"), s[t].className = y, s[t].src = r[t], s[t].onload = n[t].handleImageLoad, i[t].appendChild(s[t])
        }

        function T(e, t) {
            var n = e.collections,
                o = n.sourcesLoadsHandlers,
                s = (n.sourcesStylers, e.elements),
                i = s.sources,
                r = s.sourcesInners,
                c = e.props,
                a = c.sources;
            i[t] = document.createElement("video"), i[t].className = y, i[t].src = a[t], i[t].onloadedmetadata = function (e) {
                o[t].handleVideoLoad(e)
            }, i[t].controls = !0, c.videosPosters[t] && (i[t].poster = c.videosPosters[t]);
            var l = document.createElement("source");
            l.src = a[t], i[t].appendChild(l), setTimeout((function () {
                o[t].handleNotMetaDatedVideoLoad()
            }), 3e3), r[t].appendChild(i[t])
        }

        function A(e, t) {
            var n = e.collections.sourcesLoadsHandlers,
                o = e.elements,
                i = o.sources,
                r = o.sourcesInners,
                c = e.props.sources;
            i[t] = document.createElement("iframe"), i[t].className = "".concat(y, " ").concat(s, "youtube-iframe"), i[t].src = "https://www.youtube.com/embed/".concat(c[t].match(/^.*(youtu.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=)([^#\&\?]*).*/)[2]), i[t].allowFullscreen = !0, r[t].appendChild(i[t]), n[t].handleYoutubeLoad()
        }

        function O(e, t) {
            var n = e.collections.sourcesLoadsHandlers,
                o = e.elements,
                s = o.sources,
                i = o.sourcesInners,
                r = e.props,
                c = r.sources;
            s[t] = c[t], s[t].classList.add(y), r.customClasses[t] && s[t].classList.add(r.customClasses[t]), i[t].appendChild(s[t]), n[t].handleCustomLoad()
        }

        function N(e, t) {
            var n = e.elements,
                o = n.sources,
                i = n.sourcesInners,
                r = n.sourcesOuters;
            e.props.sources;
            o[t] = document.createElement("div"), o[t].className = "".concat(s, "invalid-file-wrapper ").concat(a), o[t].innerHTML = "Invalid source", i[t].classList.add(m), i[t].appendChild(o[t]), r[t].removeChild(r[t].firstChild)
        }

        function z(e) {
            var t = e.collections,
                n = t.sourcesLoadsHandlers,
                o = t.sourcesRenderFunctions,
                s = e.core.sourceDisplayFacade,
                i = e.resolve;
            this.runActionsForSourceTypeAndIndex = function (t, r) {
                var c;
                switch ("invalid" !== t && (n[r] = i(I, [r])), t) {
                    case "image":
                        c = E;
                        break;
                    case "video":
                        c = T;
                        break;
                    case "youtube":
                        c = A;
                        break;
                    case "custom":
                        c = O;
                        break;
                    default:
                        c = N
                }
                o[r] = function () {
                    return c(e, r)
                }, s.displaySourcesWhichShouldBeDisplayed()
            }
        }

        function H() {
            var e, t, n, o, s, i = {
                isUrlYoutubeOne: function (e) {
                    var t = document.createElement("a");
                    return t.href = e, "www.youtube.com" === t.hostname
                },
                getTypeFromResponseContentType: function (e) {
                    return e.slice(0, e.indexOf("/"))
                }
            };
            this.setUrlToCheck = function (t) {
                e = t
            }, this.getSourceType = function (t) {
                if (i.isUrlYoutubeOne(e)) return t("youtube");
                n = t, (o = new XMLHttpRequest).open("GET", e, !0), o.onreadystatechange = r, o.send()
            };
            var r = function () {
                if (4 === o.readyState && 0 === o.status && !s) return c();
                if (2 === o.readyState) {
                    if (200 !== o.status && 206 !== o.status) return s = !0, c();
                    s = !0, l(i.getTypeFromResponseContentType(o.getResponseHeader("content-type"))), a()
                }
            },
                c = function () {
                    t = "invalid", a()
                },
                a = function () {
                    o.abort(), n(t)
                },
                l = function (e) {
                    switch (e) {
                        case "image":
                            t = "image";
                            break;
                        case "video":
                            t = "video";
                            break;
                        default:
                            t = "invalid"
                    }
                }
        }

        function k(e, t, n) {
            var o = e.props,
                s = o.types,
                i = o.type,
                r = o.sources,
                c = e.resolve;
            this.getTypeSetByClientForIndex = function (e) {
                var t;
                return s && s[e] ? t = s[e] : i && (t = i), t
            }, this.retrieveTypeWithXhrForIndex = function (e) {
                var o = c(H);
                o.setUrlToCheck(r[e]), o.getSourceType((function (o) {
                    t.handleReceivedSourceTypeForUrl(o, r[e]), n.runActionsForSourceTypeAndIndex(o, e)
                }))
            }
        }

        function D(e, t) {
            var n = e.elements,
                o = n.sourcesOutersWrapper,
                s = n.sourcesOuters;
            s[t] = document.createElement("div"), s[t].className = "".concat(S, " ").concat(d, " ").concat(c, " ").concat(a), s[t].innerHTML = '<div class="fslightbox-loader"><div></div><div></div><div></div><div></div></div>', o.appendChild(s[t]),
                function (e, t) {
                    var n = e.elements,
                        o = n.sourcesOuters,
                        s = n.sourcesInners;
                    s[t] = document.createElement("div"), o[t].appendChild(s[t])
                }(e, t)
        }

        function R(e, t, n, o) {
            var i = document.createElementNS("http://www.w3.org/2000/svg", "svg");
            i.setAttributeNS(null, "width", t), i.setAttributeNS(null, "height", t), i.setAttributeNS(null, "viewBox", n);
            var r = document.createElementNS("http://www.w3.org/2000/svg", "path");
            return r.setAttributeNS(null, "class", "".concat(s, "svg-path")), r.setAttributeNS(null, "d", o), i.appendChild(r), e.appendChild(i), i
        }

        function W(e, t) {
            var n = document.createElement("div");
            return n.className = "".concat(s, "toolbar-button ").concat(a), n.title = t, e.appendChild(n), n
        }

        function P(e, t) {
            var n = document.createElement("div");
            n.className = "".concat(s, "toolbar"), t.appendChild(n),
                function (e, t) {
                    var n, o = e.core.fullscreenToggler,
                        s = e.componentsServices,
                        i = e.data,
                        r = "M4.5 11H3v4h4v-1.5H4.5V11zM3 7h1.5V4.5H7V3H3v4zm10.5 6.5H11V15h4v-4h-1.5v2.5zM11 3v1.5h2.5V7H15V3h-4z",
                        c = "M682 342h128v84h-212v-212h84v128zM598 810v-212h212v84h-128v128h-84zM342 342v-128h84v212h-212v-84h128zM214 682v-84h212v212h-84v-128h-128z",
                        a = W(t);
                    i.isFullscreenOpen = !!document.fullscreenElement, i.isFullscreenOpen ? (n = R(a, "24px", "0 0 950 1024", c), a.title = "Exit fullscreen") : (n = R(a, "20px", "0 0 18 18", r), a.title = "Enter fullscreen"), s.enterFullscreen = function () {
                        i.isFullscreenOpen = !0, a.title = "Exit fullscreen", n.setAttributeNS(null, "width", "24px"), n.setAttributeNS(null, "height", "24px"), n.setAttributeNS(null, "viewBox", "0 0 950 1024"), n.firstChild.setAttributeNS(null, "d", c)
                    }, s.exitFullscreen = function () {
                        i.isFullscreenOpen = !1, a.title = "Enter fullscreen", n.setAttributeNS(null, "width", "20px"), n.setAttributeNS(null, "height", "20px"), n.setAttributeNS(null, "viewBox", "0 0 18 18"), n.firstChild.setAttributeNS(null, "d", r)
                    }, a.onclick = function () {
                        i.isFullscreenOpen ? (s.exitFullscreen(), o.exitFullscreen()) : (s.enterFullscreen(), o.enterFullscreen())
                    }
                }(e, n),
                function (e, t) {
                    var n = W(t, "Close");
                    n.onclick = e.core.lightboxCloser.close, R(n, "20px", "0 0 24 24", "M 4.7070312 3.2929688 L 3.2929688 4.7070312 L 10.585938 12 L 3.2929688 19.292969 L 4.7070312 20.707031 L 12 13.414062 L 19.292969 20.707031 L 20.707031 19.292969 L 13.414062 12 L 20.707031 4.7070312 L 19.292969 3.2929688 L 12 10.585938 L 4.7070312 3.2929688 z")
                }(e, n)
        }

        function M(e) {
            var t = e.props.sources,
                n = e.elements.container,
                o = document.createElement("div");
            o.className = "".concat(s, "nav"), n.appendChild(o), P(e, o), t.length > 1 && function (e, t) {
                var n = e.componentsServices,
                    o = e.props.sources,
                    i = (e.stageIndexes, document.createElement("div"));
                i.className = "".concat(s, "slide-number-container");
                var r = document.createElement("div");
                r.className = a;
                var c = document.createElement("span");
                n.setSlideNumber = function (e) {
                    return c.innerHTML = e
                };
                var l = document.createElement("span");
                l.className = "".concat(s, "slash");
                var u = document.createElement("div");
                u.innerHTML = o.length, i.appendChild(r), r.appendChild(c), r.appendChild(l), r.appendChild(u), t.appendChild(i), setTimeout((function () {
                    r.offsetWidth > 55 && (i.style.justifyContent = "flex-start")
                }))
            }(e, o)
        }

        function j(e, t) {
            var n = this,
                o = e.elements.sourcesOuters,
                s = e.props,
                i = 0;
            this.byValue = function (e) {
                return i = e, n
            }, this.negative = function () {
                r(-c())
            }, this.zero = function () {
                r(0)
            }, this.positive = function () {
                r(c())
            };
            var r = function (e) {
                o[t].style.transform = "translateX(".concat(e + i, "px)"), i = 0
            },
                c = function () {
                    return (1 + s.slideDistance) * innerWidth
                }
        }

        function X(e, t, n, o) {
            var s = e.elements.container,
                i = n.charAt(0).toUpperCase() + n.slice(1),
                r = document.createElement("div");
            r.className = "".concat(f, " ").concat(f, "-").concat(n), r.title = "".concat(i, " slide"), r.onclick = t,
                function (e, t) {
                    var n = document.createElement("div");
                    n.className = "".concat(p, " ").concat(a), R(n, "20px", "0 0 20 20", t), e.appendChild(n)
                }(r, o), s.appendChild(r)
        }

        function B(e, t) {
            var n = e.classList;
            n.contains(t) && n.remove(t)
        }

        function V(e) {
            var t = this,
                n = e.core,
                o = n.eventsDispatcher,
                s = n.fullscreenToggler,
                i = n.globalEventsController,
                r = n.scrollbarRecompensor,
                c = e.data,
                a = e.elements,
                u = e.slideSwipingProps;
            this.isLightboxFadingOut = !1, this.runActions = function () {
                t.isLightboxFadingOut = !0, a.container.classList.add(v), i.removeListeners(), c.isFullscreenOpen && s.exitFullscreen(), setTimeout((function () {
                    t.isLightboxFadingOut = !1, u.isSwiping = !1, a.container.classList.remove(v), document.documentElement.classList.remove(l), r.removeRecompense(), document.body.removeChild(a.container), o.dispatch("onClose")
                }), 220)
            }
        }

        function U(e) {
            var t, n, o, s = e.collections.sourcesOutersTransformers,
                i = e.componentsServices,
                r = e.core,
                c = r.classFacade,
                a = r.slideIndexChanger,
                l = r.sourceDisplayFacade,
                d = r.stageManager,
                p = e.elements.sourcesInners,
                f = e.stageIndexes,
                v = (t = function () {
                    c.removeFromEachElementClassIfContains("sourcesInners", g)
                }, n = 250, o = [], function () {
                    o.push(!0), setTimeout((function () {
                        o.pop(), o.length || t()
                    }), n)
                });
            a.changeTo = function (e) {
                f.current = e, d.updateStageIndexes(), i.setSlideNumber(e + 1), l.displaySourcesWhichShouldBeDisplayed()
            }, a.jumpTo = function (e) {
                var t = f.current;
                a.changeTo(e), c.removeFromEachElementClassIfContains("sourcesOuters", u), B(p[t], m), B(p[t], h), p[t].classList.add(g), B(p[e], m), B(p[e], g), p[e].classList.add(h), v(), s[e].zero(), setTimeout((function () {
                    t !== f.current && s[t].negative()
                }), 220)
            }
        }

        function Y(e) {
            return e.touches ? e.touches[0].clientX : e.clientX
        }

        function q(e) {
            var t = e.componentsServices,
                n = e.core,
                o = n.lightboxCloser,
                s = n.fullscreenToggler,
                i = n.slideChangeFacade;
            this.listener = function (e) {
                switch (e.keyCode) {
                    case 27:
                        o.close();
                        break;
                    case 37:
                        i.changeToPrevious();
                        break;
                    case 39:
                        i.changeToNext();
                        break;
                    case 122:
                        e.preventDefault(), t.enterFullscreen(), s.enterFullscreen()
                }
            }
        }

        function _(e) {
            var t = e.collections.sourcesOutersTransformers,
                n = e.elements,
                o = e.slideSwipingProps,
                s = e.stageIndexes;
            this.runActionsForEvent = function (e) {
                var t, c, a;
                n.container.contains(n.slideSwipingHoverer) || n.container.appendChild(n.slideSwipingHoverer), t = n.container, c = r, (a = t.classList).contains(c) || a.add(c), o.swipedX = Y(e) - o.downClientX, i(s.current, "zero"), void 0 !== s.previous && o.swipedX > 0 ? i(s.previous, "negative") : void 0 !== s.next && o.swipedX < 0 && i(s.next, "positive")
            };
            var i = function (e, n) {
                t[e].byValue(o.swipedX)[n]()
            }
        }

        function J(e) {
            var t, n = e.props.sources,
                o = e.resolve,
                s = e.slideSwipingProps,
                i = o(_),
                r = (t = !1, function () {
                    return !t && (t = !0, requestAnimationFrame((function () {
                        t = !1
                    })), !0)
                });
            1 === n.length ? this.listener = function () {
                s.swipedX = 1
            } : this.listener = function (e) {
                s.isSwiping && r() && i.runActionsForEvent(e)
            }
        }

        function G(e) {
            var t = e.collections.sourcesOutersTransformers,
                n = e.core.slideIndexChanger,
                o = e.elements.sourcesOuters,
                s = e.stageIndexes;
            this.runPositiveSwipedXActions = function () {
                void 0 === s.previous ? i("zero") : (i("positive"), n.changeTo(s.previous), i("zero"))
            }, this.runNegativeSwipedXActions = function () {
                void 0 === s.next ? i("zero") : (i("negative"), n.changeTo(s.next), i("zero"))
            };
            var i = function (e) {
                o[s.current].classList.add(u), t[s.current][e]()
            }
        }

        function Z(e, t) {
            e.contains(t) && e.removeChild(t)
        }

        function K(e) {
            var t = e.core.lightboxCloser,
                n = e.elements,
                o = e.resolve,
                s = e.slideSwipingProps,
                i = o(G);
            this.runNoSwipeActions = function () {
                Z(n.container, n.slideSwipingHoverer), s.isSourceDownEventTarget || t.close(), s.isSwiping = !1
            }, this.runActions = function () {
                s.swipedX > 0 ? i.runPositiveSwipedXActions() : i.runNegativeSwipedXActions(), Z(n.container, n.slideSwipingHoverer), n.container.classList.remove(r), s.isSwiping = !1
            }
        }

        function Q(e) {
            var t = e.resolve,
                n = e.slideSwipingProps,
                o = t(K);
            this.listener = function () {
                n.isSwiping && (n.swipedX ? o.runActions() : o.runNoSwipeActions())
            }
        }

        function $(e) {
            var t, n, o;
            n = (t = e).core.classFacade, o = t.elements, n.removeFromEachElementClassIfContains = function (e, t) {
                for (var n = 0; n < o[e].length; n++) B(o[e][n], t)
            },
                function (e) {
                    var t = e.core.eventsDispatcher,
                        n = e.props;
                    t.dispatch = function (e) {
                        n[e] && n[e]()
                    }
                }(e),
                function (e) {
                    var t = e.core.fullscreenToggler;
                    t.enterFullscreen = function () {
                        var e = document.documentElement;
                        e.requestFullscreen ? e.requestFullscreen() : e.mozRequestFullScreen ? e.mozRequestFullScreen() : e.webkitRequestFullscreen ? e.webkitRequestFullscreen() : e.msRequestFullscreen && e.msRequestFullscreen()
                    }, t.exitFullscreen = function () {
                        document.exitFullscreen ? document.exitFullscreen() : document.mozCancelFullScreen ? document.mozCancelFullScreen() : document.webkitExitFullscreen ? document.webkitExitFullscreen() : document.msExitFullscreen && document.msExitFullscreen()
                    }
                }(e),
                function (e) {
                    var t = e.core,
                        n = t.globalEventsController,
                        o = t.windowResizeActioner,
                        s = e.resolve,
                        i = s(q),
                        r = s(J),
                        c = s(Q);
                    n.attachListeners = function () {
                        document.addEventListener("mousemove", r.listener), document.addEventListener("touchmove", r.listener, {
                            passive: !0
                        }), document.addEventListener("mouseup", c.listener), document.addEventListener("touchend", c.listener, {
                            passive: !0
                        }), addEventListener("resize", o.runActions), document.addEventListener("keydown", i.listener)
                    }, n.removeListeners = function () {
                        document.removeEventListener("mousemove", r.listener), document.removeEventListener("touchmove", r.listener), document.removeEventListener("mouseup", c.listener), document.removeEventListener("touchend", c.listener), removeEventListener("resize", o.runActions), document.removeEventListener("keydown", i.listener)
                    }
                }(e),
                function (e) {
                    var t = e.core.lightboxCloser,
                        n = (0, e.resolve)(V);
                    t.close = function () {
                        n.isLightboxFadingOut || n.runActions()
                    }
                }(e), te(e),
                function (e) {
                    var t = e.data,
                        n = e.core.scrollbarRecompensor;
                    n.addRecompense = function () {
                        "complete" === document.readyState ? o() : addEventListener("load", (function () {
                            o(), n.addRecompense = o
                        }))
                    };
                    var o = function () {
                        document.body.offsetHeight > innerHeight && (document.body.style.marginRight = t.scrollbarWidth + "px")
                    };
                    n.removeRecompense = function () {
                        document.body.style.removeProperty("margin-right")
                    }
                }(e),
                function (e) {
                    var t = e.core,
                        n = t.slideChangeFacade,
                        o = t.slideIndexChanger,
                        s = t.stageManager;
                    e.props.sources.length > 1 ? (n.changeToPrevious = function () {
                        o.jumpTo(s.getPreviousSlideIndex())
                    }, n.changeToNext = function () {
                        o.jumpTo(s.getNextSlideIndex())
                    }) : (n.changeToPrevious = function () { }, n.changeToNext = function () { })
                }(e), U(e),
                function (e) {
                    var t = e.core,
                        n = t.classFacade,
                        o = t.slideSwipingDown,
                        s = e.elements.sources,
                        i = e.slideSwipingProps,
                        r = e.stageIndexes;
                    o.listener = function (e) {
                        i.isSwiping = !0, i.downClientX = Y(e), i.swipedX = 0, "VIDEO" === e.target.tagName || e.touches || e.preventDefault();
                        var t = s[r.current];
                        t && t.contains(e.target) ? i.isSourceDownEventTarget = !0 : i.isSourceDownEventTarget = !1, n.removeFromEachElementClassIfContains("sourcesOuters", u)
                    }
                }(e),
                function (e) {
                    var t = e.collections.sourcesRenderFunctions,
                        n = e.core.sourceDisplayFacade,
                        o = e.props.loadOnlyCurrentSource,
                        s = e.stageIndexes;

                    function i(e) {
                        t[e] && (t[e](), delete t[e])
                    }
                    n.displaySourcesWhichShouldBeDisplayed = function () {
                        if (o) i(s.current);
                        else
                            for (var e in s) i(s[e])
                    }
                }(e),
                function (e) {
                    var t = e.stageIndexes,
                        n = e.core.stageManager,
                        o = e.props.sources.length - 1;
                    n.getPreviousSlideIndex = function () {
                        return 0 === t.current ? o : t.current - 1
                    }, n.getNextSlideIndex = function () {
                        return t.current === o ? 0 : t.current + 1
                    }, n.updateStageIndexes = 0 === o ? function () { } : 1 === o ? function () {
                        0 === t.current ? (t.next = 1, delete t.previous) : (t.previous = 0, delete t.next)
                    } : function () {
                        t.previous = n.getPreviousSlideIndex(), t.next = n.getNextSlideIndex()
                    }, n.isSourceInStage = o <= 2 ? function () {
                        return !0
                    } : function (e) {
                        var n = t.current;
                        if (0 === n && e === o || n === o && 0 === e) return !0;
                        var s = n - e;
                        return -1 === s || 0 === s || 1 === s
                    }
                }(e),
                function (e) {
                    var t = e.collections,
                        n = t.sourcesOutersTransformers,
                        o = t.sourcesStylers,
                        s = e.core.windowResizeActioner,
                        i = e.componentsServices,
                        r = e.data,
                        c = e.elements.sourcesOuters,
                        a = e.props,
                        l = e.stageIndexes;
                    s.runActions = function () {
                        innerWidth < 992 ? r.maxSourceWidth = innerWidth : r.maxSourceWidth = .9 * innerWidth, r.maxSourceHeight = .9 * innerHeight, r.isFullscreenOpen && screen.height !== innerHeight && i.exitFullscreen();
                        for (var e = 0; e < a.sources.length; e++) B(c[e], u), e !== l.current && n[e].negative(), o[e] && o[e].styleSize()
                    }
                }(e)
        }

        function ee(e) {
            var t = e.core.eventsDispatcher,
                n = e.data,
                o = e.elements,
                i = e.props.sources;
            n.isInitialized = !0,
                function (e) {
                    for (var t = e.collections.sourcesOutersTransformers, n = e.props.sources, o = e.resolve, s = 0; s < n.length; s++) t[s] = o(j, [s])
                }(e), $(e), o.container = document.createElement("div"), o.container.className = "".concat(s, "container ").concat(c, " ").concat(m),
                function (e) {
                    var t = e.elements;
                    t.slideSwipingHoverer = document.createElement("div"), t.slideSwipingHoverer.className = "".concat(s, "slide-swiping-hoverer ").concat(c, " ").concat(d)
                }(e), M(e),
                function (e) {
                    var t = e.core.slideSwipingDown,
                        n = e.elements,
                        o = e.props.sources;
                    n.sourcesOutersWrapper = document.createElement("div"), n.sourcesOutersWrapper.className = "".concat(d, " ").concat(c), n.container.appendChild(n.sourcesOutersWrapper), n.sourcesOutersWrapper.addEventListener("mousedown", t.listener), n.sourcesOutersWrapper.addEventListener("touchstart", t.listener, {
                        passive: !0
                    });
                    for (var s = 0; s < o.length; s++) D(e, s)
                }(e), i.length > 1 && function (e) {
                    var t = e.core.slideChangeFacade;
                    X(e, t.changeToPrevious, "previous", "M18.271,9.212H3.615l4.184-4.184c0.306-0.306,0.306-0.801,0-1.107c-0.306-0.306-0.801-0.306-1.107,0L1.21,9.403C1.194,9.417,1.174,9.421,1.158,9.437c-0.181,0.181-0.242,0.425-0.209,0.66c0.005,0.038,0.012,0.071,0.022,0.109c0.028,0.098,0.075,0.188,0.142,0.271c0.021,0.026,0.021,0.061,0.045,0.085c0.015,0.016,0.034,0.02,0.05,0.033l5.484,5.483c0.306,0.307,0.801,0.307,1.107,0c0.306-0.305,0.306-0.801,0-1.105l-4.184-4.185h14.656c0.436,0,0.788-0.353,0.788-0.788S18.707,9.212,18.271,9.212z"), X(e, t.changeToNext, "next", "M1.729,9.212h14.656l-4.184-4.184c-0.307-0.306-0.307-0.801,0-1.107c0.305-0.306,0.801-0.306,1.106,0l5.481,5.482c0.018,0.014,0.037,0.019,0.053,0.034c0.181,0.181,0.242,0.425,0.209,0.66c-0.004,0.038-0.012,0.071-0.021,0.109c-0.028,0.098-0.075,0.188-0.143,0.271c-0.021,0.026-0.021,0.061-0.045,0.085c-0.015,0.016-0.034,0.02-0.051,0.033l-5.483,5.483c-0.306,0.307-0.802,0.307-1.106,0c-0.307-0.305-0.307-0.801,0-1.105l4.184-4.185H1.729c-0.436,0-0.788-0.353-0.788-0.788S1.293,9.212,1.729,9.212z")
                }(e),
                function (e) {
                    for (var t = e.props.sources, n = e.resolve, o = n(L), s = n(z), i = n(k, [o, s]), r = 0; r < t.length; r++)
                        if ("string" == typeof t[r]) {
                            var c = i.getTypeSetByClientForIndex(r);
                            if (c) s.runActionsForSourceTypeAndIndex(c, r);
                            else {
                                var a = o.getSourceTypeFromLocalStorageByUrl(t[r]);
                                a ? s.runActionsForSourceTypeAndIndex(a, r) : i.retrieveTypeWithXhrForIndex(r)
                            }
                        } else s.runActionsForSourceTypeAndIndex("custom", r)
                }(e), t.dispatch("onInit")
        }

        function te(e) {
            var t = e.collections.sourcesOutersTransformers,
                n = e.componentsServices,
                o = e.core,
                s = o.eventsDispatcher,
                i = o.lightboxOpener,
                r = o.globalEventsController,
                c = o.scrollbarRecompensor,
                a = o.sourceDisplayFacade,
                u = o.stageManager,
                d = o.windowResizeActioner,
                p = e.data,
                f = e.elements,
                h = e.stageIndexes;
            i.open = function () {
                var o = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : 0;
                h.current = o, p.isInitialized ? s.dispatch("onShow") : ee(e), u.updateStageIndexes(), a.displaySourcesWhichShouldBeDisplayed(), n.setSlideNumber(h.current + 1), document.body.appendChild(f.container), document.documentElement.classList.add(l), c.addRecompense(), r.attachListeners(), s.dispatch("onOpen"), t[h.current].zero(), d.runActions()
            }
        }

        function ne() {
            var e = localStorage.getItem("fslightbox-scrollbar-width");
            if (e) return e;
            var t = function () {
                var e = document.createElement("div"),
                    t = e.style;
                return t.visibility = "hidden", t.width = "100px", t.msOverflowStyle = "scrollbar", t.overflow = "scroll", e
            }(),
                n = function () {
                    var e = document.createElement("div");
                    return e.style.width = "100%", e
                }();
            document.body.appendChild(t);
            var o = t.offsetWidth;
            t.appendChild(n);
            var s = n.offsetWidth;
            document.body.removeChild(t);
            var i = o - s;
            return localStorage.setItem("fslightbox-scrollbar-width", i.toString()), i
        }

        function oe() {
            if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
            if (Reflect.construct.sham) return !1;
            if ("function" == typeof Proxy) return !0;
            try {
                return Date.prototype.toString.call(Reflect.construct(Date, [], (function () { }))), !0
            } catch (e) {
                return !1
            }
        }

        function se(e, t, n) {
            return (se = oe() ? Reflect.construct : function (e, t, n) {
                var o = [null];
                o.push.apply(o, t);
                var s = new (Function.bind.apply(e, o));
                return n && ie(s, n.prototype), s
            }).apply(null, arguments)
        }

        function ie(e, t) {
            return (ie = Object.setPrototypeOf || function (e, t) {
                return e.__proto__ = t, e
            })(e, t)
        }

        function re(e) {
            return function (e) {
                if (Array.isArray(e)) {
                    for (var t = 0, n = new Array(e.length); t < e.length; t++) n[t] = e[t];
                    return n
                }
            }(e) || function (e) {
                if (Symbol.iterator in Object(e) || "[object Arguments]" === Object.prototype.toString.call(e)) return Array.from(e)
            }(e) || function () {
                throw new TypeError("Invalid attempt to spread non-iterable instance")
            }()
        }

        function ce() {
            for (var e = document.getElementsByTagName("a"), t = function (t) {
                if (!e[t].hasAttribute("data-fslightbox")) return "continue";
                var n = e[t].getAttribute("data-fslightbox"),
                    o = e[t].getAttribute("href");
                fsLightboxInstances[n] || (fsLightboxInstances[n] = new FsLightbox, fsLightboxInstances[n].setup());
                var s = null;
                s = "#" === o.charAt(0) ? document.getElementById(o.substring(1)) : o, fsLightboxInstances[n].props.sources.push(s), fsLightboxInstances[n].elements.a.push(e[t]);
                var i = fsLightboxInstances[n].props.sources.length - 1;

                function r(o, s) {
                    e[t].hasAttribute(s) && (fsLightboxInstances[n].props[o][i] = e[t].getAttribute(s))
                }
                e[t].onclick = function (e) {
                    e.preventDefault(), fsLightboxInstances[n].open(i)
                }, r("types", "data-type"), r("videosPosters", "data-video-poster"), r("customClasses", "data-custom-class")
            }, n = 0; n < e.length; n++) t(n);
            var o = Object.keys(fsLightboxInstances);
            window.fsLightbox = fsLightboxInstances[o[o.length - 1]]
        }
        window.FsLightbox = function () {
            var e = this;
            this.props = {
                sources: [],
                customClasses: [],
                types: [],
                videosPosters: [],
                maxYoutubeDimensions: null,
                slideDistance: .3
            }, this.data = {
                isInitialized: !1,
                maxSourceWidth: 0,
                maxSourceHeight: 0,
                scrollbarWidth: ne(),
                isFullscreenOpen: !1
            }, this.slideSwipingProps = {
                isSwiping: !1,
                downClientX: null,
                isSourceDownEventTarget: !1,
                swipedX: 0
            }, this.stageIndexes = {}, this.elements = {
                a: [],
                container: null,
                slideSwipingHoverer: null,
                sourcesOutersWrapper: null,
                sources: [],
                sourcesOuters: [],
                sourcesInners: []
            }, this.componentsServices = {
                setSlideNumber: function () { },
                enterFullscreen: null,
                exitFullscreen: null
            }, this.resolve = function (t) {
                var n = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : [];
                return n.unshift(e), se(t, re(n))
            }, this.collections = {
                sourcesOutersTransformers: [],
                sourcesLoadsHandlers: [],
                sourcesRenderFunctions: [],
                sourcesStylers: []
            }, this.core = {
                classFacade: {},
                eventsDispatcher: {},
                fullscreenToggler: {},
                globalEventsController: {},
                lightboxCloser: {},
                lightboxOpener: {},
                lightboxUpdater: {},
                scrollbarRecompensor: {},
                slideChangeFacade: {},
                slideIndexChanger: {},
                slideSwipingDown: {},
                sourceDisplayFacade: {},
                stageManager: {},
                windowResizeActioner: {}
            }, this.setup = function () {
                te(e)
            }, this.open = function (t) {
                return e.core.lightboxOpener.open(t)
            }, this.close = function () {
                return e.core.lightboxCloser.close()
            }
        }, window.fsLightboxInstances = {}, window.refreshFsLightbox = function () {
            for (var e in fsLightboxInstances) {
                var t = fsLightboxInstances[e].props;
                fsLightboxInstances[e] = new FsLightbox, fsLightboxInstances[e].props = t, fsLightboxInstances[e].props.sources = [], fsLightboxInstances[e].elements.a = [], fsLightboxInstances[e].setup()
            }
            ce()
        }, ce()
    }])
}));