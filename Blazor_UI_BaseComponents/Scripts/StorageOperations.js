window.layoutStore =
{
    saveLocal: function (key, obj) {
        localStorage.setItem(key, JSON.stringify(obj));
    },

    loadLocal: function (key) {
        const s = localStorage.getItem(key);
        return s ? JSON.parse(s) : null;
    },


};

//window.cookieStore =
//{
//    setCookie: function (name, value, days)
//    {
//        let expires = "";
//        if (days)
//        {
//            const d = new Date();
//            d.setTime(d.getTime() + days * 24 * 60 * 60 * 1000);
//            expires = "; expires=" + d.toUTCString();
//        }
//        // NOTE: cannot set HttpOnly from JS. Secure requires HTTPS
//        document.cookie = `${name}=${encodeURIComponent(value)}${expires}; path=/; SameSite=Strict`;
//    },

//    getCookie: function (name)
//    {
//        const match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
//        return match ? decodeURIComponent(match[2]) : null;
//    }
//};