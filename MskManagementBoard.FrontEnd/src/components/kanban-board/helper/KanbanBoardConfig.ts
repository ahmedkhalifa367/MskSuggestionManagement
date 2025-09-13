import { Status } from '@/models';
import { RiskLevel } from '@/models/RiskLevel';
import type { CardSettingsModel, DialogFieldsModel, DialogSettingsModel } from '@syncfusion/ej2-vue-kanban';

export class KanbanBoardConfig {
  public static readonly cardSettings: Readonly<CardSettingsModel> = Object.freeze({
    headerField: 'Title',
    contentField: 'Description',
    template: 'cardTemplate',
  });
  public static readonly dialogSettings:  Readonly<DialogSettingsModel> = Object.freeze({
    template: "dialogTemplate",
    model: {
      header: 'Card Details',
      isModal: true,
      showCloseIcon: true,
      allowDragging: true,
      enableResize: true,
    }  
  });

  public static badgeStatusClass(status: Status): string {
    const map: Record<Status, string> = {
      New: 'badge badge-status-new',
      Assigned: 'badge badge-status-assigned',
      InProgress: 'badge badge-status-inprogress',
      Completed: 'badge badge-status-completed',
      Rejected: 'badge badge-status-rejected',
    };
    return map[status] ?? 'badge';
  }

  public static headerClassName(data: any): string {
    const map: Record<Status, string> = {
      New: 'header-icon e-icons New',
      Assigned: 'header-icon e-icons Assigned',
      InProgress: 'header-icon e-icons InProgress',
      Completed: 'header-icon e-icons Completed',
      Rejected: 'header-icon e-icons Rejected',
    };
    return map[data.keyField] ?? 'badge';
  }

  public static badgeLevelClass(level: RiskLevel): string {
    const map: Record<RiskLevel, string> = {
      Low: 'badge badge-low',
      Medium: 'badge badge-medium',
      High: 'badge badge-high',
    };
    return map[level] ?? 'badge';
  }
}
