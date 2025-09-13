<script lang="ts">
import { Component, Ref, Vue, toNative } from "vue-facing-decorator";
import {
  KanbanComponent,
  ColumnsDirective,
  ColumnDirective,
} from "@syncfusion/ej2-vue-kanban";
import type { DialogEventArgs, DragEventArgs } from '@syncfusion/ej2-kanban';
import type { ActionEventArgs } from '@syncfusion/ej2-kanban';
import { KanbanBoardConfig } from "./helper/KanbanBoardConfig";
import { IKanbanBoardMskRecommendation } from "@/models/IKanbanBoardMskRecommendation";
import { recommendationService } from "@/services/RecommendationService";
import { IKanbanCard } from "./models";
import { KanbanDataMapper } from "./helper/KanbanDataMapper";
import { Status } from "@/models/Status";
import { IEmployee } from "@/models/IEmployee";
import { employeeService } from "@/services/EmployeeService";

@Component({
  name: "KanbanBoard",
  components: {
    "ejs-kanban": KanbanComponent,
    "e-columns": ColumnsDirective,
    "e-column": ColumnDirective,
  }
})
class KanbanBoard extends Vue {
    @Ref
	public readonly kanbanControlElement!: InstanceType<typeof KanbanComponent>;
    public kanbanConfig = KanbanBoardConfig;
    public kanbanData: IKanbanCard[] = [];
    public employees: IEmployee[] = [];
    public loading = false;
    public badgeStatusClass = KanbanBoardConfig.badgeStatusClass;
    public badgeLevelClass = KanbanBoardConfig.badgeLevelClass;
    public headerClassName = KanbanBoardConfig.headerClassName;

    private async created(): Promise<void> {
        try {
            //this.loading = true;
            const mskRecommendation = await recommendationService.getMskRecommendations();
            this.kanbanData = this.combineRecommendations(mskRecommendation);
            this.employees = await employeeService.getAllEmployees();
        } catch (error) {
            console.error('Error loading MSK recommendations:', error);
        } finally {
            //this.loading = false;
        }
    }

    public onItemDragStop(args: DragEventArgs): void {
        console.log("cccccccccccccccccc")
        const card = args.data?.[0] as Partial<IKanbanCard>;
        const employeeId = card?.EmployeeId ?? null;
        if (employeeId) {
            this.updateRecommendationStatus(employeeId, card.RecommendationId, card.Status);
        }
    }

    public async onActionBegin(args: ActionEventArgs) {
        if (args.requestType === "cardChange") {
            const card = (args.changedRecords as IKanbanCard[])[0];
            const employeeId = card?.EmployeeId;
            if (card.RecommendationId && employeeId) {
                this.assignMskRecommendation(card.EmployeeId, card.RecommendationId);
                this.updateCardStatusAfterAssigned(card, employeeId);
            }
        }

        if (args.requestType === "cardRemove") {
            const card = (args.deletedRecords as IKanbanCard[])[0];
            this.deleteMskRecommendation(card.RecommendationId);
        }
    }

    private combineRecommendations(boardMskRecommendation: IKanbanBoardMskRecommendation): IKanbanCard[] {
        const combinedCards: IKanbanCard[] = [];

        if (boardMskRecommendation.vidaMskRecommendations) {
            const vidaCards = KanbanDataMapper.mapVidaMskRecommendations(boardMskRecommendation.vidaMskRecommendations);
            combinedCards.push(...vidaCards);
        }

        if (boardMskRecommendation.triggeredMskRecommendations) {
            const triggeredCards = KanbanDataMapper.mapEmployeeMskRecommendations(boardMskRecommendation.triggeredMskRecommendations);
            combinedCards.push(...triggeredCards);
        }

        return combinedCards;
    }

    private async updateRecommendationStatus(employeeId: string, mskRecommendationId: string, newStatus: Status): Promise<void> {
        console.log(newStatus)
        try {
            await recommendationService.updateMskRecommendationStatus({
                employeeId,
                mskRecommendationId,
                newStatus
            });
        } catch (error) {
            console.error('Failed to update status:', error);
        }
    }

    private async assignMskRecommendation(employeeId: string, mskRecommendationId: string): Promise<void> {
        try {
            await recommendationService.assignMskRecommendation({
                employeeId,
                mskRecommendationId
            });
        } catch (error) {
            console.error('Failed to Assign:', error);
        }
    }

    private async deleteMskRecommendation(mskRecommendationId: string): Promise<void> {
        throw new Error("Not implemented yet");
    }

    private updateCardStatusAfterAssigned(card: IKanbanCard, employeeId: string) {
        const cardIndex = this.kanbanData.findIndex(c => c.RecommendationId === card.RecommendationId);
        if (cardIndex !== -1){
            const employee = this.employees.find(e => e.id === employeeId);
            card.Status = "Assigned";
            card.EmployeeFullName = employee.fullName;
            this.refreshKanbanBoard();
        }
    }

    private refreshKanbanBoard(): void {
        // TODO: Debug reference issue needs further investigation and improvement.
        const kanbanControlElement = Ref(null);
        let schedule = kanbanControlElement.value.ej2Instances;
        schedule.refreshLayout();
    }
}
export default toNative(KanbanBoard);
</script>
<template>
    <section class="panel">
        <ejs-kanban
            ref="kanbanControlElement"
            keyField="Status"
            style="padding-top: 10px;"
            width="auto"
            height="auto"
            cssClass="kanban-header-template"
            :dataSource="kanbanData"
            :cardSettings="kanbanConfig.cardSettings"
            :dialogSettings="kanbanConfig.dialogSettings"
            :allowDragAndDrop="true"
            :dragStop="onItemDragStop"
            :actionBegin="onActionBegin"
            >
            <template #cardTemplate="{ data }">
                <div class="card">
                    <div class="card-top">
                        <div class="card-title">{{ data.Title }}</div>
                    </div>
                    <div class="card-row">
                        <span class="label">Risk Level:</span>
                        <span :class="badgeLevelClass(data.Level)">{{ data.Level }}</span>
                    </div>
                    <div class="card-row">
                        <span class="label">Description:</span>
                        <span class="value">{{ data.Description }}</span>
                    </div>

                    <div class="card-row" v-if="data.EmployeeFullName">
                        <span class="label">Assignee:</span>
                        <span class="value">{{ data.EmployeeFullName }}</span>
                    </div>
                </div>
            </template>
            <template #dialogTemplate="{ data }" >
                <div class="card">
                    <div class="card-row">
                        <span class="label e-label">Risk Level:</span>
                        <span :class="badgeLevelClass(data.Level)">{{ data.Level }}</span>
                    </div>
                    <div class="card-row">
                        <span class="label e-label">Status:</span>
                        <span :class="badgeStatusClass(data.Status)">{{ data.Status }}</span>
                    </div>
                    <div class="card-row">
                        <span class="label e-label">Description:</span>
                        <span class="value">{{ data.Description }}</span>
                    </div>
                    <div class="card-row">
                        <span class="label e-label">Employee:</span>
                        <select v-if="data.Status === 'New'" class="base-select" v-model="data.EmployeeId">
                            <option :value="null">Select employee</option>
                            <option v-for="employee in employees" :key="employee.id" :value="employee.id">
                                {{ employee.fullName }}
                            </option>
                        </select>
                        <span v-else>{{ data.EmployeeFullName }}</span>
                    </div>
                </div>
            </template>

            <e-columns>
                <e-column headerText="New" keyField="New" :allowToggle="true" :template="'columnsTemplate'"></e-column>
                <e-column headerText="Assigned" keyField="Assigned" :allowToggle="true" :template="'columnsTemplate'"></e-column>
                <e-column headerText="In Progress" keyField="InProgress" :allowToggle="true" :template="'columnsTemplate'"></e-column>
                <e-column headerText="Completed" keyField="Completed" :allowToggle="true" :template="'columnsTemplate'"></e-column>
                <e-column headerText="Rejected" keyField="Rejected" :allowToggle="true" :template="'columnsTemplate'"></e-column>
            </e-columns>
            <template #columnsTemplate="{data}">
                <div class="header-template-wrap">
                    <div :class="headerClassName(data)"></div>
                    <div class="header-text">{{ data.headerText }}</div>
                </div>
            </template>
        </ejs-kanban>
    </section>
</template>

<style scoped>
.panel {
    flex: 1;
    width: 100%;
    height: 100%;
    min-height: 0;
    border-radius: 1rem;
    background: linear-gradient(180deg,#eff6fc 0%,#f7f8fa 100%);
    padding: 0.5rem;
    overflow: auto;
}

.card {
    line-height: 1.35;
    padding: 1rem;
    display: flex;
    flex-direction: column;
    gap: .5rem;
}

.card-title {
    font-weight: 600;
    color: #6b7280;
}

.card-row {
    display: grid;
    grid-template-columns: max-content 1fr;
    column-gap: 8px;
    align-items: start;
    margin: 6px 0;
    font-size: 13px;
}

.label {
    color: #6b7280;
    font-weight: 600;
    width: 5rem;
}

.value {
    color: #374151;
    overflow-wrap: anywhere;
}

.base-select{
  width: 100%;
  padding: 0.5rem 0.75rem;
  border: 1px solid #e5e7eb;
  border-radius: 0.5rem;
  background:#fff;
  color:#111827;
  font-size:14px;
}

.base-select:focus{
    outline: #e5e7eb auto;
}

@media (max-width: 640px){
    :deep(.card-row) {
        grid-template-columns: 1fr;
    }
    :deep(.label) {
        width:auto;
    }
}

</style>
