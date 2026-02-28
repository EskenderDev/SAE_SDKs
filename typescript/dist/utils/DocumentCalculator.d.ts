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
    descuentos?: {
        monto: number;
    }[];
    impuestos?: {
        tarifa: number;
        monto?: number;
        exoneracion?: {
            montoExoneracion: number;
        };
    }[];
    impuestoAsumidoEmisorFabrica?: number;
    montoTotal?: number;
    subTotal?: number;
    impuestoNeto?: number;
    montoTotalLinea?: number;
    unidadMedida?: string;
}
export interface LineItemCalculated extends LineItemInput {
    montoTotal: number;
    subTotal: number;
    impuestoNeto: number;
    montoTotalLinea: number;
}
export declare class DocumentCalculator {
    /**
     * Rounds a number to a specified number of decimal places (default 5 for Hacienda)
     * using standard half-up rounding to emulate C# Math.Round behavior.
     */
    static roundHacienda(value: number, decimals?: number): number;
    /**
     * Calculates missing totals for a single line item, exactly as SAE.GENERATE does.
     */
    static calculateLineItem(linea: LineItemInput): LineItemCalculated;
    /**
     * Processes an array of line items and calculates all missing totals for each.
     */
    static calculateLines(lineas: LineItemInput[]): LineItemCalculated[];
}
