import { Component } from "@angular/core";
import { KPI_DATA, KpiRow } from "../data/kpi.data";
import { CommonModule } from "@angular/common";
@Component({
  selector: "app-bsc-kpi",
  standalone: true,
  imports: [CommonModule],
  templateUrl: "./bsc-kpi.component.html",
  styleUrl: "./bsc-kpi.component.css",
})
export class BscKpiComponent {
  kpis = KPI_DATA;
  days = Array.from({ length: 10 }, (_, i) => i + 1);

  expanded = new Set<KpiRow>();

  toggle(row: KpiRow) {
    this.expanded.has(row) ? this.expanded.delete(row) : this.expanded.add(row);
  }

  isExpanded(row: KpiRow) {
    return this.expanded.has(row);
  }

  cellColor(value: number, target: number): string {
    if (value >= target) return "bg-green-500";
    if (value >= target * 0.8) return "bg-yellow-400";
    return "bg-red-500";
  }
}
