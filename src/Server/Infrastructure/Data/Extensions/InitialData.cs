// Copyright(c) 2025 - Jun Dev.All rights reserved

namespace Infrastructure.Data.Extensions;

internal static class InitialData
{
	/// <summary>Scripts to create and update the search vector for the task table</summary>
	public static string[] TaskSearchVectorScripts = new[]
	{
		@"ALTER TABLE task ADD COLUMN IF NOT EXISTS search_vector tsvector;",

		@"CREATE OR REPLACE FUNCTION update_search_vector() RETURNS trigger AS $$
			BEGIN
				NEW.search_vector := to_tsvector('simple', coalesce(NEW.title, '') || ' ' || coalesce(NEW.description, ''));
				RETURN NEW;
			END;
			$$ LANGUAGE plpgsql;",

		@"DROP TRIGGER IF EXISTS trg_update_search_vector ON task;",

		@"CREATE TRIGGER trg_update_search_vector
			BEFORE INSERT OR UPDATE ON task
			FOR EACH ROW EXECUTE FUNCTION update_search_vector();",

		@"CREATE INDEX IF NOT EXISTS ix_task_search_vector ON task USING GIN(search_vector);"
	};
}
