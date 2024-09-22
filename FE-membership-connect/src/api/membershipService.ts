export interface Membership {
	id: number;
	name: string;
	description: string;
	price: number;
	durationInMonths: number;
}

const API_BASE_URL =
	process.env.REACT_APP_API_BASE_URL || 'http://localhost:5000';

export const membershipService = {
	async getMemberships(): Promise<Membership[]> {
		const response = await fetch(`${API_BASE_URL}/Memberships`);
		if (!response.ok) {
			throw new Error('Failed to fetch memberships');
		}
		return response.json();
	},

	async getMembershipById(id: number): Promise<Membership> {
		const response = await fetch(`${API_BASE_URL}/Memberships/${id}`, {
			method: 'PUT',
			headers: {
				Accept: 'application/json',
			},
		});
		if (!response.ok) {
			throw new Error('Failed to fetch membership');
		}
		return response.json();
	},

	async addMembership(membership: Omit<Membership, 'id'>): Promise<string> {
		const response = await fetch(`${API_BASE_URL}/Memberships`, {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json',
			},
			body: JSON.stringify(membership),
		});
		if (!response.ok) {
			throw new Error('Failed to add membership');
		}
		const responseText = await response.text();
		if (responseText !== 'Successfully created membership') {
			throw new Error('Unexpected response from server');
		}
		return responseText;
	},

	async deleteMembership(id: number): Promise<void> {
		const response = await fetch(`${API_BASE_URL}/Memberships/${id}`, {
			method: 'DELETE',
		});
		if (!response.ok) {
			throw new Error('Failed to delete membership');
		}
	},
};
