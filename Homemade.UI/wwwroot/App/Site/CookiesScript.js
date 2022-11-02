const rand = () => Math.random(0).toString(36).substr(2);
const token = (length) => (rand() + rand() + rand() + rand()).substr(0, length);
debugger;
let cookie_consent = cart_getCookie("user_cookie_consent");
if (cookie_consent != "" && cookie_consent != undefined) {
    document.getElementById("cookieNotice").style.display = "none";
} else {
    document.getElementById("cookieNotice").style.display = "block";
}
// Create cookie
function cart_setCookie(name, value, exp) { Cookies.set(name, value, { expires: exp }); }
// Read cookie by name
function cart_getCookie(name) { return Cookies.get(name); }
// Delete cookie
function cart_removeCookie(name) { Cookies.remove(name) }
// Set cookie consent
function acceptCookieConsent() {
    cart_removeCookie('user_cookie_consent');
    cart_setCookie('user_cookie_consent', 1, 30);
    document.getElementById("cookieNotice").style.display = "none";
    // create client-side token
    cart_setCookie("sessionToken", token(40), 7);
    // create server-side token
    CreateSession();
}
// end cookie Section
