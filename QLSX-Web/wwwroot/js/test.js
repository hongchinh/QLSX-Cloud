export class DOMCleanup {
    static observer;

    static createObserver() {
        const target = document.querySelector('#cleanupDiv');

        this.observer = new MutationObserver(function (mutations) {
            const targetRemoved = mutations.some(function (mutation) {
                const nodes = Array.from(mutation.removedNodes);
                return nodes.indexOf(target) !== -1;
            });

            if (targetRemoved) {
                // Cleanup resources here
                // ...

                // Disconnect and delete MutationObserver
                this.observer && this.observer.disconnect();
                delete this.observer;
            }
        });

        this.observer.observe(target.parentNode, { childList: true });
    }
    static myJavaScriptFunction2() {
        // JavaScript logic here
        console.log("JavaScript function invoked from Blazor! 22222222");
    }
}

window.DOMCleanup = DOMCleanup;