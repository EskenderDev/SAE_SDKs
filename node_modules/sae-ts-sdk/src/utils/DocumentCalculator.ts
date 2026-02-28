/**
 * Utility for accurately calculating invoice totals based on Hacienda CR rules.
 * Emulates the internal logic of SAE.GENERATE to ensure the client-side totals 
 * match the backend exactly before submitting.
 * 
 * Uses 5-decimal precision logic as expected by Hacienda.
 */

export interface LineItemInput {
    cantidad: number;
    precioUnitario: number;
    descuentos?: { monto: number }[];
    impuestos?: { tarifa: number; monto?: number; exoneracion?: { montoExoneracion: number } }[];
    impuestoAsumidoEmisorFabrica?: number;
    montoTotal?: number;
    subTotal?: number;
    impuestoNeto?: number;
    montoTotalLinea?: number;
    unidadMedida?: string; // Optional, used for category calc if doing full Resumen
}

export interface LineItemCalculated extends LineItemInput {
    montoTotal: number;
    subTotal: number;
    impuestoNeto: number;
    montoTotalLinea: number;
}

export class DocumentCalculator {
    /**
     * Rounds a number to a specified number of decimal places (default 5 for Hacienda)
     * using standard half-up rounding to emulate C# Math.Round behavior.
     */
    public static roundHacienda(value: number, decimals: number = 5): number {
        const factor = Math.pow(10, decimals);
        return Math.round(value * factor) / factor;
    }

    /**
     * Calculates missing totals for a single line item, exactly as SAE.GENERATE does.
     */
    public static calculateLineItem(linea: LineItemInput): LineItemCalculated {
        // 1. Calculate base amounts if not provided
        let montoTotal = linea.montoTotal !== undefined ? linea.montoTotal : this.roundHacienda(linea.cantidad * linea.precioUnitario);

        let totalDescuentos = 0;
        if (linea.descuentos && linea.descuentos.length > 0) {
            totalDescuentos = linea.descuentos.reduce((sum, d) => sum + (d.monto || 0), 0);
        }

        // 2. SubTotal
        let subTotal = linea.subTotal !== undefined ? linea.subTotal : this.roundHacienda(montoTotal - totalDescuentos);

        // 3. Impuestos
        let totalImpuesto = 0;
        let totalExoneracion = 0;

        if (linea.impuestos && linea.impuestos.length > 0) {
            linea.impuestos.forEach(i => {
                // Auto calculate tax amount if missing based on subtotal
                if (i.monto === undefined || i.monto === 0) {
                    i.monto = this.roundHacienda(subTotal * (i.tarifa || 0) / 100);
                }

                totalImpuesto += i.monto;

                if (i.exoneracion) {
                    totalExoneracion += i.exoneracion.montoExoneracion || 0;
                }
            });
        }

        // 4. Impuesto Neto
        let impuestoAsumido = linea.impuestoAsumidoEmisorFabrica || 0;
        let impuestoNeto = linea.impuestoNeto !== undefined
            ? linea.impuestoNeto
            : this.roundHacienda(totalImpuesto - totalExoneracion - impuestoAsumido);

        // 5. Monto Total Linea
        let montoTotalLinea = linea.montoTotalLinea !== undefined
            ? linea.montoTotalLinea
            : this.roundHacienda(subTotal + impuestoNeto);

        return {
            ...linea,
            montoTotal,
            subTotal,
            impuestoNeto,
            montoTotalLinea
        };
    }

    /**
     * Processes an array of line items and calculates all missing totals for each.
     */
    public static calculateLines(lineas: LineItemInput[]): LineItemCalculated[] {
        return lineas.map(l => this.calculateLineItem(l));
    }
}
