export interface CreateVacancy {
    title: string;
    description: string;
    maxSalary: number;
    workExperience: number;
    assignedRecruiterId: number;
    seniorityLevelId: number;
    jobTypeId: number;
    vacancyStatusId: number;
    picture: File;
}