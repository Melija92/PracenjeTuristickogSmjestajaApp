function PocetnoSlovo(imeIliPrezime) {
    if (imeIliPrezime[0] != imeIliPrezime[0].toUpperCase()) {

        alert("Prvo početno slovo mora biti veliko za unos ".concat(imeIliPrezime))
        return false;
    }
    else {
        return true;
    }
}


function OibDuzina(oib) {
    if (oib.length != 11) {
        alert("OIB mora sadržavati točno 11 brojeva");
        return false;
    }
    else {
        return true;
    }
}
function OibBrojevi(oib) {
    if (/^\d+$/.test(oib)) {
        return true;
    }
    else {
        alert("OIB mora sadržavati samo brojeve");
        return false;
    }
}

function DatumValidacija(datum) {
    var danas = new Date();
    danas.setHours(0, 0, 0, 0);
    var unos = new Date(datum);

    if (unos >= danas) {
        alert("Datum rođenja mora biti prije današnjeg!");
        return false;
    }
    else {
        return true;
    }
}

function UsporedbaDatuma(prvi, drugi) {
    var pocetak = new Date(prvi);
    pocetak.setHours(0, 0, 0, 0);
    var kraj = new Date(drugi);
    kraj.setHours(0, 0, 0, 0);

    if (pocetak >= kraj) {
        alert("Početak iznajmljivanja ne može biti nakon završetka iznajmljivanja!");
        return false;
    }
    else {
        return true;
    }
}

function TuristValidacija() {

    var validacija = true;
    var oibTurista = document.forms["turist_forma"]["OIB_turista"].value;
    var imeTurista = document.forms["turist_forma"]["Ime"].value;
    var prezimeTurista = document.forms["turist_forma"]["Prezime"].value;
    var datumTurista = document.forms["turist_forma"]["DatumRodenja"].value;


    var prolasci = [OibDuzina(oibTurista), OibBrojevi(oibTurista), PocetnoSlovo(imeTurista), PocetnoSlovo(prezimeTurista), DatumValidacija(datumTurista)];
    prolasci.forEach(function (prolazak) {
        if (prolazak == false)
            validacija = false;
    });
    return validacija;
}


function VlasnikValidacija() {

    var validacija = true;
    var oibVlasnika = document.forms["vlasnik_forma"]["OIB_vlasnika"].value;
    var imeVlasnika = document.forms["vlasnik_forma"]["Ime"].value;
    var prezimeVlasnika = document.forms["vlasnik_forma"]["Prezime"].value;

    var prolasci = [OibDuzina(oibVlasnika), OibBrojevi(oibVlasnika), PocetnoSlovo(imeVlasnika), PocetnoSlovo(prezimeVlasnika)];

    prolasci.forEach(function (prolazak) {
        if (prolazak == false)
            validacija = false;
    });
    return validacija;


}

function IznajmljivanjeValidacija() {
    var pocetak = document.forms["iznajmljivanje_forma"]["PocetakIznajmljivanja"].value;
    var kraj = document.forms["iznajmljivanje_forma"]["ZavrsetakIznajmljivanja"].value;

    var validacija = UsporedbaDatuma(pocetak, kraj);
    return validacija;
}

setTimeout(function () {
    $('#successMessage').remove();
}, 3000);