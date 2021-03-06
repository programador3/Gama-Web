(function (c) { typeof module === "object" && module.exports ? module.exports = c : c(Highcharts) })(function (c) {
    function y(a, b, d, f, h) { for (var h = h || 0, f = f || z, c = h + f, n = !0; n && h < c && h < a.length;) n = b(a[h], h), h += 1; n && (h < a.length ? setTimeout(function () { y(a, b, d, f, h) }) : d && d()) } var r = c.win.document, Q = function () { }, R = c.Color, k = c.Series, e = c.seriesTypes, l = c.each, s = c.extend, S = c.addEvent, T = c.fireEvent, t = c.grep, U = c.merge, V = c.pick, j = c.wrap, p = c.getOptions().plotOptions, z = 5E4, A; l(["area", "arearange", "column", "line", "scatter"], function (a) {
        if (p[a]) p[a].boostThreshold =
        5E3
    }); l(["translate", "generatePoints", "drawTracker", "drawPoints", "render"], function (a) {
        function b(b) { var f = this.options.stacking && (a === "translate" || a === "generatePoints"); if ((this.processedXData || this.options.data).length < (this.options.boostThreshold || Number.MAX_VALUE) || f) { if (a === "render" && this.image) this.image.attr({ href: "" }), this.animate = null; b.call(this) } else if (this[a + "Canvas"]) this[a + "Canvas"]() } j(k.prototype, a, b); a === "translate" && (e.column && j(e.column.prototype, a, b), e.arearange && j(e.arearange.prototype,
        a, b))
    }); j(k.prototype, "getExtremes", function (a) { this.hasExtremes() || a.apply(this, Array.prototype.slice.call(arguments, 1)) }); j(k.prototype, "setData", function (a) { this.hasExtremes(!0) || a.apply(this, Array.prototype.slice.call(arguments, 1)) }); j(k.prototype, "processData", function (a) { this.hasExtremes(!0) || a.apply(this, Array.prototype.slice.call(arguments, 1)) }); c.extend(k.prototype, {
        pointRange: 0, allowDG: !1, hasExtremes: function (a) {
            var b = this.options, d = this.xAxis && this.xAxis.options, f = this.yAxis && this.yAxis.options;
            return b.data.length > (b.boostThreshold || Number.MAX_VALUE) && typeof f.min === "number" && typeof f.max === "number" && (!a || typeof d.min === "number" && typeof d.max === "number")
        }, destroyGraphics: function () { var a = this, b = this.points, d, f; if (b) for (f = 0; f < b.length; f += 1) if ((d = b[f]) && d.graphic) d.graphic = d.graphic.destroy(); l(["graph", "area", "tracker"], function (b) { a[b] && (a[b] = a[b].destroy()) }) }, getContext: function () {
            var a = this.chart, b = a.plotWidth, d = a.plotHeight, f = this.ctx, h = function (a, b, d, f, h, c, e) {
                a.call(this, d, b, f, h, c,
                e)
            }; this.canvas ? f.clearRect(0, 0, b, d) : (this.canvas = r.createElement("canvas"), this.image = a.renderer.image("", 0, 0, b, d).add(this.group), this.ctx = f = this.canvas.getContext("2d"), a.inverted && l(["moveTo", "lineTo", "rect", "arc"], function (a) { j(f, a, h) })); this.canvas.width = b; this.canvas.height = d; this.image.attr({ width: b, height: d }); return f
        }, canvasToSVG: function () { this.image.attr({ href: this.canvas.toDataURL("image/png") }) }, cvsLineTo: function (a, b, d) { a.lineTo(b, d) }, renderCanvas: function () {
            var a = this, b = a.options,
            d = a.chart, f = this.xAxis, h = this.yAxis, c, e = 0, j = a.processedXData, k = a.processedYData, l = b.data, i = f.getExtremes(), p = i.min, r = i.max, i = h.getExtremes(), t = i.min, W = i.max, B = {}, u, X = !!a.sampling, C, D = b.marker && b.marker.radius, E = this.cvsDrawPoint, F = b.lineWidth ? this.cvsLineTo : !1, G = D <= 1 ? this.cvsMarkerSquare : this.cvsMarkerCircle, Y = b.enableMouseTracking !== !1, H, i = b.threshold, m = h.getThreshold(i), I = typeof i === "number", J = m, Z = this.fill, K = a.pointArrayMap && a.pointArrayMap.join(",") === "low,high", L = !!b.stacking, $ = a.cropStart ||
            0, i = d.options.loading, aa = a.requireSorting, M, ba = b.connectNulls, N = !j, v, w, o, q, ca = a.fillOpacity ? (new R(a.color)).setOpacity(V(b.fillOpacity, 0.75)).get() : a.color, O = function () { Z ? (c.fillStyle = ca, c.fill()) : (c.strokeStyle = a.color, c.lineWidth = b.lineWidth, c.stroke()) }, P = function (a, b, d) { e === 0 && c.beginPath(); M ? c.moveTo(a, b) : E ? E(c, a, b, d, H) : F ? F(c, a, b) : G && G(c, a, b, D); e += 1; e === 1E3 && (O(), e = 0); H = { clientX: a, plotY: b, yBottom: d } }, x = function (a, b, c) {
                Y && !B[a + "," + b] && (B[a + "," + b] = !0, d.inverted && (a = f.len - a, b = h.len - b), C.push({
                    clientX: a,
                    plotX: a, plotY: b, i: $ + c
                }))
            }; (this.points || this.graph) && this.destroyGraphics(); a.plotGroup("group", "series", a.visible ? "visible" : "hidden", b.zIndex, d.seriesGroup); a.getAttribs(); a.markerGroup = a.group; S(a, "destroy", function () { a.markerGroup = null }); C = this.points = []; c = this.getContext(); a.buildKDTree = Q; if (l.length > 99999) d.options.loading = U(i, { labelStyle: { backgroundColor: "rgba(255,255,255,0.75)", padding: "1em", borderRadius: "0.5em" }, style: { backgroundColor: "none", opacity: 1 } }), clearTimeout(A), d.showLoading("Drawing..."),
            d.options.loading = i; y(L ? a.data : j || l, function (b, c) {
                var e, g, j, i, l = typeof d.index === "undefined", n = !0; if (!l) {
                    N ? (e = b[0], g = b[1]) : (e = b, g = k[c]); if (K) N && (g = b.slice(1, 3)), i = g[0], g = g[1]; else if (L) e = b.x, g = b.stackY, i = g - b.y; j = g === null; aa || (n = g >= t && g <= W); if (!j && e >= p && e <= r && n) if (e = Math.round(f.toPixels(e, !0)), X) {
                        if (o === void 0 || e === u) { K || (i = g); if (q === void 0 || g > w) w = g, q = c; if (o === void 0 || i < v) v = i, o = c } e !== u && (o !== void 0 && (g = h.toPixels(w, !0), m = h.toPixels(v, !0), P(e, I ? Math.min(g, J) : g, I ? Math.max(m, J) : m), x(e, g, q), m !== g && x(e,
                        m, o)), o = q = void 0, u = e)
                    } else g = Math.round(h.toPixels(g, !0)), P(e, g, m), x(e, g, c); M = j && !ba; c % z === 0 && a.canvasToSVG()
                } return !l
            }, function () { var b = d.loadingDiv, c = d.loadingShown; O(); a.canvasToSVG(); T(a, "renderedCanvas"); if (c) s(b.style, { transition: "opacity 250ms", opacity: 0 }), d.loadingShown = !1, A = setTimeout(function () { b.parentNode && b.parentNode.removeChild(b); d.loadingDiv = d.loadingSpan = null }, 250); a.directTouch = !1; a.options.stickyTracking = !0; delete a.buildKDTree; a.buildKDTree() }, d.renderer.forExport ? Number.MAX_VALUE :
            void 0)
        }
    }); e.scatter.prototype.cvsMarkerCircle = function (a, b, d, c) { a.moveTo(b, d); a.arc(b, d, c, 0, 2 * Math.PI, !1) }; e.scatter.prototype.cvsMarkerSquare = function (a, b, d, c) { a.rect(b - c, d - c, c * 2, c * 2) }; e.scatter.prototype.fill = !0; s(e.area.prototype, { cvsDrawPoint: function (a, b, d, c, e) { e && b !== e.clientX && (a.moveTo(e.clientX, e.yBottom), a.lineTo(e.clientX, e.plotY), a.lineTo(b, d), a.lineTo(b, c)) }, fill: !0, fillOpacity: !0, sampling: !0 }); s(e.column.prototype, { cvsDrawPoint: function (a, b, d, c) { a.rect(b - 1, d, 1, c - d) }, fill: !0, sampling: !0 });
    k.prototype.getPoint = function (a) { var b = a; if (a && !(a instanceof this.pointClass)) b = (new this.pointClass).init(this, this.options.data[a.i]), b.category = b.x, b.dist = a.dist, b.distX = a.distX, b.plotX = a.plotX, b.plotY = a.plotY; return b }; j(k.prototype, "destroy", function (a) { var b = this, c = b.chart; if (c.hoverPoints) c.hoverPoints = t(c.hoverPoints, function (a) { return a.series === b }); if (c.hoverPoint && c.hoverPoint.series === b) c.hoverPoint = null; a.call(this) }); j(k.prototype, "searchPoint", function (a) {
        return this.getPoint(a.apply(this,
        [].slice.call(arguments, 1)))
    })
});