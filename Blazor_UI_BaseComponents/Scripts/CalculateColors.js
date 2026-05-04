window.getHexColorCode = function (element) {
    try {
        if (!element) {
            console.error("No element passed");
            return null;
        }

        const style = getComputedStyle(element);
        const color = style.backgroundColor;
        var result = convertRGBToHex(color);

        if (!result) {
            console.error("No result");
            return null;
        }

        return result;
    }
    catch (e) {
        console.error("getHexColorCode error:", e);
        return null;
    }
}

window.convertRGBToHex = function (rgb) {
    try {
        const match = rgb.match(/\d+/g);
        if (!match) return null;
        var result = (
            "#" +
            match
                .slice(0, 3)
                .map(x => Number.parseInt(x).toString(16).padStart(2, "0"))
                .join("")
        );

        if (!result) {
            console.error("No result");
            return null;
        }

        return result;
    }
    catch (e) {
        console.error("convertRGBToHex error:", e);
        return null;
    }
}