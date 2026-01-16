import { Component } from "@angular/core";
import { BSC_DATA, KPI, Perspective } from "../data/bsc-data";
import { CommonModule } from "@angular/common";
import { BSC_QUADRANTS } from "../data/bsc-quadrant.data";
@Component({
  selector: "app-bsc-component",
  standalone: true,
  imports: [CommonModule],
  templateUrl: "./bsc-component.component.html",
  styleUrl: "./bsc-component.component.css",
})
export class BscComponent {
  perspectives: Perspective[] = BSC_DATA;
  selectedKpi: KPI | null = null;
  panelMode: "view" | "edit" = "view";
  quadrants = BSC_QUADRANTS;

  toggle(item: any) {
    item.expanded = !item.expanded;
  }

  openDetails(kpi: any, mode: "view" | "edit" = "view") {
    this.selectedKpi = { ...kpi }; // clone for edit safety
    this.panelMode = mode;
  }

  closeDetails() {
    this.selectedKpi = null;
  }

  getStatus(kpi: KPI) {
    return kpi.actual >= kpi.target ? "On Track" : "Off Track";
  }
  getProgress(kpi: any): number {
    if (!kpi || !kpi.target) return 0;
    return Math.min((kpi.actual / kpi.target) * 100, 100);
  }

  getPercent(i: any): number {
    return Math.round((i.result / i.target) * 100);
  }

  getStatusColor(i: any): string {
    const p = this.getPercent(i);
    if (p >= 100) return "bg-green-500";
    if (p >= 90) return "bg-yellow-400";
    return "bg-red-500";
  }
}
