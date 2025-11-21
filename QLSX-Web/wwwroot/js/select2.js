(function (window) {
    window.logUser =
        window.logUser ||
        function (counter) {
            console.log(`Printing in JavaScript counter: ${counter}`);
        }

    window.getFormattedMessage =
        window.getFormattedMessage ||
        function () {
            return "Hello from JavaScript to C#";
        }

    window.invokeDotnetStaticFunction =
        window.invokeDotnetStaticFunction ||
        function () {
            DotNet.invokeMethodAsync('Blazor.Demo', 'HelpMessage')
                .then(data => { console.log(data); });
        }

    window.invokeDotnetInstanceFunction =
        window.invokeDotnetInstanceFunction ||
        function (addressProvider) {
            addressProvider.invokeMethodAsync("GetAddress")
                .then(data => { console.log(data); });
        }


    window.initselect2 =
        window.initselect2 ||
        function () {
            $("#simpleSelect2").select2({
                placeholder: "Select a Static Value",
                theme: "bootstrap4",
                allowClear: true
            });
        }
    window.initselect2Ajax =
        window.initselect2Ajax ||
    function () {

            getData();

            $("#ajaxMultiSelect2").select2({
                placeholder: "Select Multiple Values",
                theme: "bootstrap4",
                allowClear: true,
                data: getData()
            });
        }
    window.getData =
        window.getData ||
        function () {
            DotNet.invokeMethodAsync('CRMApp', 'HelpMessage')
                .then(data => {
                    console.log(data);
                    return data;
                });
        }
})(window);