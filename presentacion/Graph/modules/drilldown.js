(function (e) { typeof module === "object" && module.exports ? module.exports = e : e(Highcharts) })(function (e) {
    function A(a, b, d) { var c; !b.rgba.length || !a.rgba.length ? a = b.input || "none" : (a = a.rgba, b = b.rgba, c = b[3] !== 1 || a[3] !== 1, a = (c ? "rgba(" : "rgb(") + Math.round(b[0] + (a[0] - b[0]) * (1 - d)) + "," + Math.round(b[1] + (a[1] - b[1]) * (1 - d)) + "," + Math.round(b[2] + (a[2] - b[2]) * (1 - d)) + (c ? "," + (b[3] + (a[3] - b[3]) * (1 - d)) : "") + ")"); return a } var u = function () { }, q = e.getOptions(), i = e.each, l = e.extend, B = e.format, v = e.pick, r = e.wrap, m = e.Chart, p = e.seriesTypes,
    w = p.pie, n = p.column, x = e.Tick, s = e.fireEvent, y = e.inArray, z = 1; i(["fill", "stroke"], function (a) { e.Fx.prototype[a + "Setter"] = function () { this.elem.attr(a, A(e.Color(this.start), e.Color(this.end), this.pos)) } }); l(q.lang, { drillUpText: "\u25c1 Back to {series.name}" }); q.drilldown = {
        activeAxisLabelStyle: { cursor: "pointer", color: "#0d233a", fontWeight: "bold", textDecoration: "underline" }, activeDataLabelStyle: { cursor: "pointer", color: "#0d233a", fontWeight: "bold", textDecoration: "underline" }, animation: { duration: 500 }, drillUpButton: {
            position: {
                align: "right",
                x: -10, y: 10
            }
        }
    }; e.SVGRenderer.prototype.Element.prototype.fadeIn = function (a) { this.attr({ opacity: 0.1, visibility: "inherit" }).animate({ opacity: v(this.newOpacity, 1) }, a || { duration: 250 }) }; m.prototype.addSeriesAsDrilldown = function (a, b) { this.addSingleSeriesAsDrilldown(a, b); this.applyDrilldown() }; m.prototype.addSingleSeriesAsDrilldown = function (a, b) {
        var d = a.series, c = d.xAxis, g = d.yAxis, f; f = a.color || d.color; var h, e = [], j = [], k, o; if (!this.drilldownLevels) this.drilldownLevels = []; k = d.options._levelNumber || 0; (o = this.drilldownLevels[this.drilldownLevels.length -
        1]) && o.levelNumber !== k && (o = void 0); b = l({ color: f, _ddSeriesId: z++ }, b); h = y(a, d.points); i(d.chart.series, function (a) { if (a.xAxis === c && !a.isDrilling) a.options._ddSeriesId = a.options._ddSeriesId || z++, a.options._colorIndex = a.userOptions._colorIndex, a.options._levelNumber = a.options._levelNumber || k, o ? (e = o.levelSeries, j = o.levelSeriesOptions) : (e.push(a), j.push(a.options)) }); f = {
            levelNumber: k, seriesOptions: d.options, levelSeriesOptions: j, levelSeries: e, shapeArgs: a.shapeArgs, bBox: a.graphic ? a.graphic.getBBox() : {}, color: f,
            lowerSeriesOptions: b, pointOptions: d.options.data[h], pointIndex: h, oldExtremes: { xMin: c && c.userMin, xMax: c && c.userMax, yMin: g && g.userMin, yMax: g && g.userMax }
        }; this.drilldownLevels.push(f); f = f.lowerSeries = this.addSeries(b, !1); f.options._levelNumber = k + 1; if (c) c.oldPos = c.pos, c.userMin = c.userMax = null, g.userMin = g.userMax = null; if (d.type === f.type) f.animate = f.animateDrilldown || u, f.options.animation = !0
    }; m.prototype.applyDrilldown = function () {
        var a = this.drilldownLevels, b; if (a && a.length > 0) b = a[a.length - 1].levelNumber,
        i(this.drilldownLevels, function (a) { a.levelNumber === b && i(a.levelSeries, function (a) { a.options && a.options._levelNumber === b && a.remove(!1) }) }); this.redraw(); this.showDrillUpButton()
    }; m.prototype.getDrilldownBackText = function () { var a = this.drilldownLevels; if (a && a.length > 0) return a = a[a.length - 1], a.series = a.seriesOptions, B(this.options.lang.drillUpText, a) }; m.prototype.showDrillUpButton = function () {
        var a = this, b = this.getDrilldownBackText(), d = a.options.drilldown.drillUpButton, c, g; this.drillUpButton ? this.drillUpButton.attr({ text: b }).align() :
        (g = (c = d.theme) && c.states, this.drillUpButton = this.renderer.button(b, null, null, function () { a.drillUp() }, c, g && g.hover, g && g.select).attr({ align: d.position.align, zIndex: 9 }).add().align(d.position, !1, d.relativeTo || "plotBox"))
    }; m.prototype.drillUp = function () {
        for (var a = this, b = a.drilldownLevels, d = b[b.length - 1].levelNumber, c = b.length, g = a.series, f, h, e, j, k = function (b) {
        var c; i(g, function (a) { a.options._ddSeriesId === b._ddSeriesId && (c = a) }); c = c || a.addSeries(b, !1); if (c.type === e.type && c.animateDrillupTo) c.animate = c.animateDrillupTo;
        b === h.seriesOptions && (j = c)
        }; c--;) if (h = b[c], h.levelNumber === d) {
            b.pop(); e = h.lowerSeries; if (!e.chart) for (f = g.length; f--;) if (g[f].options.id === h.lowerSeriesOptions.id && g[f].options._levelNumber === d + 1) { e = g[f]; break } e.xData = []; i(h.levelSeriesOptions, k); s(a, "drillup", { seriesOptions: h.seriesOptions }); if (j.type === e.type) j.drilldownLevel = h, j.options.animation = a.options.drilldown.animation, e.animateDrillupFrom && e.chart && e.animateDrillupFrom(h); j.options._levelNumber = d; e.remove(!1); if (j.xAxis) f = h.oldExtremes,
            j.xAxis.setExtremes(f.xMin, f.xMax, !1), j.yAxis.setExtremes(f.yMin, f.yMax, !1)
        } s(a, "drillupall"); this.redraw(); this.drilldownLevels.length === 0 ? this.drillUpButton = this.drillUpButton.destroy() : this.drillUpButton.attr({ text: this.getDrilldownBackText() }).align(); this.ddDupes.length = []
    }; n.prototype.supportsDrilldown = !0; n.prototype.animateDrillupTo = function (a) {
        if (!a) {
            var b = this, d = b.drilldownLevel; i(this.points, function (a) { a.graphic && a.graphic.hide(); a.dataLabel && a.dataLabel.hide(); a.connector && a.connector.hide() });
            setTimeout(function () { b.points && i(b.points, function (a, b) { var f = b === (d && d.pointIndex) ? "show" : "fadeIn", e = f === "show" ? !0 : void 0; if (a.graphic) a.graphic[f](e); if (a.dataLabel) a.dataLabel[f](e); if (a.connector) a.connector[f](e) }) }, Math.max(this.chart.options.drilldown.animation.duration - 50, 0)); this.animate = u
        }
    }; n.prototype.animateDrilldown = function (a) {
        var b = this, d = this.chart.drilldownLevels, c, e = this.chart.options.drilldown.animation, f = this.xAxis; if (!a) i(d, function (a) {
            if (b.options._ddSeriesId === a.lowerSeriesOptions._ddSeriesId) c =
            a.shapeArgs, c.fill = a.color
        }), c.x += v(f.oldPos, f.pos) - f.pos, i(this.points, function (a) { a.graphic && a.graphic.attr(c).animate(l(a.shapeArgs, { fill: a.color }), e); a.dataLabel && a.dataLabel.fadeIn(e) }), this.animate = null
    }; n.prototype.animateDrillupFrom = function (a) {
        var b = this.chart.options.drilldown.animation, d = this.group, c = this; i(c.trackerGroups, function (a) { if (c[a]) c[a].on("mouseover") }); delete this.group; i(this.points, function (c) {
            var f = c.graphic, h = function () { f.destroy(); d && (d = d.destroy()) }; f && (delete c.graphic,
            b ? f.animate(l(a.shapeArgs, { fill: a.color }), e.merge(b, { complete: h })) : (f.attr(a.shapeArgs), h()))
        })
    }; w && l(w.prototype, {
        supportsDrilldown: !0, animateDrillupTo: n.prototype.animateDrillupTo, animateDrillupFrom: n.prototype.animateDrillupFrom, animateDrilldown: function (a) {
            var b = this.chart.drilldownLevels[this.chart.drilldownLevels.length - 1], d = this.chart.options.drilldown.animation, c = b.shapeArgs, g = c.start, f = (c.end - g) / this.points.length; if (!a) i(this.points, function (a, i) {
                a.graphic.attr(e.merge(c, {
                    start: g + i * f, end: g +
                    (i + 1) * f, fill: b.color
                }))[d ? "animate" : "attr"](l(a.shapeArgs, { fill: a.color }), d)
            }), this.animate = null
        }
    }); e.Point.prototype.doDrilldown = function (a, b, d) {
        var c = this.series.chart, e = c.options.drilldown, f = (e.series || []).length, h; if (!c.ddDupes) c.ddDupes = []; for (; f-- && !h;) e.series[f].id === this.drilldown && y(this.drilldown, c.ddDupes) === -1 && (h = e.series[f], c.ddDupes.push(this.drilldown)); s(c, "drilldown", { point: this, seriesOptions: h, category: b, originalEvent: d, points: b !== void 0 && this.series.xAxis.ddPoints[b].slice(0) },
        function (b) { var d = b.point.series && b.point.series.chart, c = b.seriesOptions; d && c && (a ? d.addSingleSeriesAsDrilldown(b.point, c) : d.addSeriesAsDrilldown(b.point, c)) })
    }; e.Axis.prototype.drilldownCategory = function (a, b) { var d, c, e = this.ddPoints[a]; for (d in e) (c = e[d]) && c.series && c.series.visible && c.doDrilldown && c.doDrilldown(!0, a, b); this.chart.applyDrilldown() }; e.Axis.prototype.getDDPoints = function (a, b) { var d = this.ddPoints; if (!d) this.ddPoints = d = {}; d[a] || (d[a] = []); if (d[a].levelNumber !== b) d[a].length = 0; return d[a] };
    x.prototype.drillable = function () { var a = this.pos, b = this.label, d = this.axis, c = d.ddPoints && d.ddPoints[a]; if (b && c && c.length) { if (!b.basicStyles) b.basicStyles = e.merge(b.styles); b.addClass("highcharts-drilldown-axis-label").css(d.chart.options.drilldown.activeAxisLabelStyle).on("click", function (b) { d.drilldownCategory(a, b) }) } else if (b && b.basicStyles) b.styles = {}, b.css(b.basicStyles), b.on("click", null) }; r(x.prototype, "addLabel", function (a) { a.call(this); this.drillable() }); r(e.Point.prototype, "init", function (a,
    b, d, c) { var g = a.call(this, b, d, c), a = (d = b.xAxis) && d.ticks[c], d = d && d.getDDPoints(c, b.options._levelNumber); if (g.drilldown && (e.addEvent(g, "click", function (a) { b.xAxis && b.chart.options.drilldown.allowPointDrilldown === !1 ? b.xAxis.drilldownCategory(c, a) : g.doDrilldown(void 0, void 0, a) }), d)) d.push(g), d.levelNumber = b.options._levelNumber; a && a.drillable(); return g }); r(e.Series.prototype, "drawDataLabels", function (a) {
        var b = this.chart.options.drilldown.activeDataLabelStyle; a.call(this); i(this.points, function (a) {
            a.drilldown &&
            a.dataLabel && a.dataLabel.attr({ "class": "highcharts-drilldown-data-label" }).css(b)
        })
    }); var t, q = function (a) { a.call(this); i(this.points, function (a) { a.drilldown && a.graphic && a.graphic.attr({ "class": "highcharts-drilldown-point" }).css({ cursor: "pointer" }) }) }; for (t in p) p[t].prototype.supportsDrilldown && r(p[t].prototype, "drawTracker", q)
});