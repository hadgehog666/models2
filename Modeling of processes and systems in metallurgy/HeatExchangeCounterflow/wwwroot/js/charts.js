function drawCharts(labels, material, gas, delta) {

    new Chart(document.getElementById("tempChart"), {
        type: "line",
        data: {
            labels: labels,
            datasets: [
                {
                    label: "Температура материала, °C",
                    data: material,
                    borderWidth: 2
                },
                {
                    label: "Температура газа, °C",
                    data: gas,
                    borderWidth: 2
                }
            ]
        }
    });

    new Chart(document.getElementById("deltaChart"), {
        type: "line",
        data: {
            labels: labels,
            datasets: [
                {
                    label: "Разность температур, °C",
                    data: delta,
                    borderWidth: 2
                }
            ]
        }
    });
}
