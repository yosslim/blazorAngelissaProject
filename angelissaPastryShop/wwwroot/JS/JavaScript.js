function scrollToCart() {
    const cart = document.getElementById('cart');
    if (cart) {
        cart.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
}