redirectToCheckout = function (sessionId) {
    var stripe = Stripe('pk_test_51Jc24bLqiV3s2KUVhYHbKAfRc0PmUJ0OlONOs6hw4De2Afu2PPld3DRoe0l7ggC08KomZhPAjaPziJQp4Wh3W5Aw00kdmJivDg');
    stripe.redirectToCheckout({
        sessionId: sessionId
    });
};